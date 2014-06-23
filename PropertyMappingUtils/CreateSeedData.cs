using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Data.Entity;

namespace PropertyMappingUtils
{
    public static class MapExistingInstaces
    {
        public enum SeedClassType { Migrations, DBinitializer}
        public static void CreateSeedFile<T>(T context, SeedClassType seedType = SeedClassType.Migrations) where T : DbContext
        {
            var dlg = new Microsoft.Win32.SaveFileDialog
            {
                Title = "Select article",
                DefaultExt = ".cs",
                Filter = "C# (*.cs)|*.cs"
            };
            if (dlg.ShowDialog()==true) 
            {
                System.IO.File.WriteAllText(dlg.FileName, ClassFromContext(context, seedType));
            }
        }
        const char indentChar = ' ';
        const int indentSize = 4;
        const string newLine = "\r\n";
        const string seedMethod = "Add"; // change to .AddOrUpdate if required
        public static string ClassFromContext<T>(T context, SeedClassType seedType = SeedClassType.Migrations) where T : DbContext
        {
            //http://stackoverflow.com/questions/10490885/how-can-i-find-all-dbsets-whose-generic-types-derive-from-a-given-base-type
            var sb = new StringBuilder();
            var tType = typeof(T);
            string singleIndent = new string(indentChar,indentSize);
            string doubleIndent = new string(indentChar, indentSize*2);
            string tripleIndent = new string(indentChar, indentSize * 3);

            if (seedType == SeedClassType.Migrations)
            {
                sb.AppendFormat("namespace {0}.Migrations{1}{{{1}", tType.Namespace, newLine);
                sb.Append(UsingStrings(singleIndent, "System", "System.Data.Entity", "System.Data.Entity.Migrations", "System.Linq"));

                sb.AppendFormat("{0}internal sealed class Configuration : DbMigrationsConfiguration<{1}>{2}", singleIndent, tType.Name, newLine);
                sb.AppendFormat("{0}{{{1}{2}public Configuration(){1}{2}{{{1}{0}{2}AutomaticMigrationsEnabled = false;{1}{2}}}{1}", singleIndent, newLine, doubleIndent);
            }
            else
            {
                sb.AppendFormat("namespace {0}{1}{{{1}", tType.Namespace, newLine);
                sb.Append(UsingStrings(singleIndent, "System", "System.Data.Entity"));
                sb.AppendFormat("{0}public class {1}SeedInitializer : CreateDatabaseIfNotExists<{1}>{2}{0}{{{2}", singleIndent,tType.Name, newLine);
            }

            sb.AppendFormat("{0}protected override void Seed({1} context){2}{0}{{{2}", doubleIndent, tType.FullName, newLine);
            var entitySets = typeof(T).GetProperties().Where(p => p.PropertyType.IsGenericType && p.PropertyType.GetGenericTypeDefinition() == typeof(DbSet<>));
            bool firstIt = (seedType == SeedClassType.Migrations);
            foreach (var p in entitySets)
            {
                var setName = "context." +  p.Name;
                if (firstIt) 
                {
                    sb.AppendFormat("{0}if ({1}.Any()){{ return; }}{2}", tripleIndent, setName, newLine);
                    firstIt = false; 
                }
                sb.Append(From((dynamic)p.GetValue(context, null), setName));
            }
            sb.AppendFormat("{0}{1}context.SaveChanges();{0}{2}}}{0}{3}}}{0}}}", newLine, tripleIndent, doubleIndent,singleIndent);
            return sb.ToString();
        }
        public static string From<TEntity>(IEnumerable<TEntity> set, string setName) where TEntity : class
        {
            Type type = typeof(TEntity);
            var dbProps = type.GetAssignablePrimativePropInfo();
            var sb = new StringBuilder();
            var level1Indent = new string(indentChar, indentSize * 3);
            var level2Indent = new string(indentChar, indentSize * 4);
            var level3Indent = new string(indentChar, indentSize * 5);
            var addStr = level1Indent + setName + "." + seedMethod + "(new " + type.Name + newLine + level2Indent + "{" + newLine;

            foreach (var instance in set)
            {
                sb.Append(addStr);

                foreach (var p in dbProps)
                {
                    sb.Append(level3Indent + p.Name + " = " + p.ToCodeString(instance) + ',' + newLine);
                }
                sb.Length-=(newLine.Length +1); //(remove last comma)
                sb.Append(newLine + level2Indent + "});" + newLine);
            }
            return sb.ToString();
        }
        public static string UsingStrings(string indent="", params string[] nameSpaces)
        {
            if (nameSpaces.Length==0) {throw new ArgumentException("namespaces must be provided");}
            return string.Join(null, nameSpaces.Select(u => string.Format("{0}using {1};{2}", indent, u, newLine)));
        }
    }
}
