using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;

namespace PropertyMappingUtils
{
    public class RegexMapExperimenter<T>
    {
        #region Members
        private Regex _regEx;
        private RegexOptions _regexOptions = RegexOptions.None;
        private string _regExString;
        private bool _attemptRegExAssign;
        private RegexPropertyMapTester<T> _propertyMap;
        private string _parseString;
        private bool _globalMatch;
        private bool _trimProperties;
        private bool _autoTruncateToLen;
        #endregion
        #region Constructors
        #endregion
        #region properties
        public RegexPropertyMapTester<T> PropertyMap
        {
            get 
            {
                if (_propertyMap == null)
                {
                    if (GetPropertyException() != null)
                    {
                        return null;
                    }
                    _propertyMap = new RegexPropertyMapTester<T>(_regEx) 
                    {
                        GlobalMatch = _globalMatch, 
                        TrimProperties=_trimProperties,
                        AutoTruncateStringToAttrLen =_autoTruncateToLen
                    };
                    if (!string.IsNullOrEmpty(ParseString)) { _propertyMap.ParseString = ParseString; }
                }
                return _propertyMap; 
            }
        }
        public bool TrimProperties
        {
            get { return _trimProperties; }
            set
            {
                _trimProperties = value;
                if (_propertyMap != null) { _propertyMap.TrimProperties = _trimProperties; }
            }
        }
        public bool AutoTruncateToAttrLen
        {
            get { return _autoTruncateToLen; }
            set
            {
                _autoTruncateToLen = value;
                if (_propertyMap != null) { _propertyMap.AutoTruncateStringToAttrLen = _autoTruncateToLen; }
            }
        }
        public bool GlobalMatch
        {
            get { return _globalMatch; }
            set
            {
                _globalMatch = value;
                if (_propertyMap != null) { _propertyMap.GlobalMatch = _globalMatch; }
            }
        }
        public string ParseString 
        { 
            get { return _parseString; }
            set 
            { 
                _parseString = value;
                if (PropertyMap != null) { _propertyMap.ParseString = _parseString; }
            }
        }
        public string RegularExpression
        {
            get
            {
                return _regExString;
            }
            set
            {
                _regExString = value;
                _propertyMap = null;
                _attemptRegExAssign = true;
                try
                {
                    _regEx = new Regex(_regExString, this.RegexOptions);
                }
                catch (ArgumentException)
                {
                    _regEx = null;
                }
            }
        }
        public bool IsValidRegEx
        {
            get { return _regEx == null; }
        }
        public RegexOptions RegexOptions
        {
            get { return _regexOptions; }
            set
            {
                if (_regexOptions != value)
                {
                    _regexOptions = value;
                    if (_regEx != null)
                    {
                        this.RegularExpression = _regExString;
                    }
                }
            }
        }
        #endregion
        #region Methods
        public override string ToString()
        {
            var ex = GetPropertyException();
            if (ex != null)
            {
                if (ex is InvalidRegularExpressionException)
                {
                    return "Regular expression is not valid";
                }
                var propEx = ex as RequiredPropertyNotSetException;
                if (propEx != null)
                {
                    if (propEx.PropertyName == "RegularExpression")
                    {
                        return "Regular expression has not been entered";
                    }
                    else if (propEx.PropertyName == "ParseString")
                    {
                        return "A test string has not been entered";
                    }
                }
                throw ex;
            }
            return PropertyMap.ToString();
        }
        private Exception GetPropertyException()
        {
            if (!_attemptRegExAssign) { return new RequiredPropertyNotSetException("RegularExpression"); }
            if (_regEx == null) { return new InvalidRegularExpressionException(); }
            if (string.IsNullOrWhiteSpace(ParseString)) { return new RequiredPropertyNotSetException("ParseString"); }
            return null;
        }
        #endregion
    }
    public class RegexPropertyMapTester<T> : RegexPropertyMap<T>
    {
        #region Constructor
        public RegexPropertyMapTester(Regex regex) : base(regex) { }
        #endregion
        # region Members
        private IDictionary<RegExPropertyMapMatchType, IEnumerable<string>> _attemptedMatchNames;
        private IDictionary<string, RegExPropertyMapMatch> _matchData;
        # endregion
        #region Properties
        public IDictionary<RegExPropertyMapMatchType, IEnumerable<string>> AttemptedMatchNames
        {
            get
            {
                if (_attemptedMatchNames == null)
                {
                    _attemptedMatchNames = new Dictionary<RegExPropertyMapMatchType, IEnumerable<string>>();
                    var groupMatches = GroupNames.ToLookup(g => PropertyInfos.ContainsKey(g),
                                                            g => g);
                    _attemptedMatchNames.Add(RegExPropertyMapMatchType.InRegexOnly, groupMatches[false]);
                    _attemptedMatchNames.Add(RegExPropertyMapMatchType.FullMatch, groupMatches[true]);
                    _attemptedMatchNames.Add(RegExPropertyMapMatchType.InPropertyOnly,
                                    PropertyInfos.Keys.Where(k => !groupMatches[true].Contains(k)));
                }
                return _attemptedMatchNames;
            }
        }
        public IDictionary<string, RegExPropertyMapMatch> MatchData
        {
            get
            {
                if (_matchData == null || !IsMatchCollectionSet)
                {
                    _matchData = new Dictionary<string, RegExPropertyMapMatch>();

                    foreach (string propName in AttemptedMatchNames[RegExPropertyMapMatchType.FullMatch])
                    {
                        var pi = PropertyInfos[propName];
                        _matchData.Add(propName, new RegExPropertyMapMatch
                        {
                            PropertyType = pi.PropertyType.ToString(),
                            MatchItem = MatchCollection.Select(m => pi.ReflectionParse(m.Groups[propName].Value)).ToArray()
                        });
                    }
                }
                return _matchData;
            }
        }
        #endregion
        #region Methods
        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append("--Matches Identified--" + Environment.NewLine);
            foreach (var m in MatchData)
            {
                sb.AppendLine(m.Key.ToString() + ':' + m.Value.ToString());
            }
            if (AttemptedMatchNames[RegExPropertyMapMatchType.InRegexOnly].Any())
            {
                sb.Append(Environment.NewLine + "!!--In Regular Expression Groups, NOT found in property--!!" + Environment.NewLine);
                sb.Append('\'' + string.Join("','", AttemptedMatchNames[RegExPropertyMapMatchType.InRegexOnly]) + '\'');
            }
            if (AttemptedMatchNames[RegExPropertyMapMatchType.InPropertyOnly].Any())
            {
                sb.Append(Environment.NewLine + "!!--In Property, NOT found in regular expression named groups--!!" + Environment.NewLine);
                sb.Append('\'' + string.Join("','", AttemptedMatchNames[RegExPropertyMapMatchType.InPropertyOnly]) + '\'');
            }
            return sb.ToString();
        }

        #endregion
    }
    public class RegexPropertyMap<T>
    {
        #region Constructor
        public RegexPropertyMap(Regex regex)
        {
            PropertyInfos = typeof(T).GetAssignablePrimativePropInfo().ToDictionary(p=>p.Name);
            _regEx = regex;
        }
        #endregion
        #region Members
        protected readonly IDictionary<string, PropertyInfo> PropertyInfos;
        private Regex _regEx;
        private IEnumerable<string> _groupNames;
        private IEnumerable<Match> _matchCollection;
        private bool _globalMatch;
        private string _parseString;
        #endregion
        #region Properties
        public bool AutoTruncateStringToAttrLen { get; set; }
        public bool TrimProperties { get; set; }
        public bool GlobalMatch { 
            get { return _globalMatch; }
            set
            {
                if (value != _globalMatch)
                {
                    _globalMatch = value;
                    _matchCollection = null;
                }
            }
        }
        public string ParseString { 
            get 
            { 
                return _parseString; 
            } 
            set 
            {
                if (value != _parseString)
                {
                    _parseString = value;
                    _matchCollection = null;
                }
            } 
        }
        protected bool IsMatchCollectionSet { get { return _matchCollection != null; } }
        protected IEnumerable<Match> MatchCollection
        {
            get
            {
                if (_matchCollection == null )
                {
                    if (ParseString == null) { throw new RequiredPropertyNotSetException("ParseString"); }
                    _matchCollection = GlobalMatch ? _regEx.Matches(ParseString).Cast<Match>() 
                                                                               : new Match[] { _regEx.Match(ParseString) };
                }
                return _matchCollection;
            }
        }
        protected IEnumerable<string> GroupNames
        {
            get
            {
                return _groupNames ?? (_groupNames = _regEx.GetGroupNames().Where(g=>!g.IsDigitsOnly()));
            }
        }

        #endregion
        #region Methods
        public void AssignTo(ref T instance)
        {
            AssignTo(ref instance, MatchCollection.FirstOrDefault());
        }
        private void AssignTo(ref T instance, Match match)
        {
            foreach (var groupName in GroupNames)
            {
                PropertyInfo prop;
                if (PropertyInfos.TryGetValue(groupName, out prop))
                {
                    string foundText = match.Groups[groupName].Value;
                    if (TrimProperties) { foundText = foundText.Trim(); }
                    prop.AssignFromString(instance, foundText, AutoTruncateStringToAttrLen);
                }
            }
        }
        public List<T> ToList()
        {
            var returnVal = new List<T>();
            foreach (Match match in MatchCollection)
            {
                var newInst = Activator.CreateInstance<T>();
                AssignTo(ref newInst, match);
                returnVal.Add(newInst);
            }
            return returnVal;
        }
        #endregion
    }
    public class InvalidRegularExpressionException : Exception
    {
        public InvalidRegularExpressionException(string msg = "Invalid Regular Expression"):base(msg){}
    }
    public class RequiredPropertyNotSetException : Exception
    {
        public RequiredPropertyNotSetException(string propertyName) : base("Required property: \'" + propertyName + "\' has not been assigned a value") 
        {
            PropertyName = propertyName;
        }
        public readonly string PropertyName;
    }

    public enum RegExPropertyMapMatchType { FullMatch, InPropertyOnly, InRegexOnly}

    public class RegExPropertyMapMatch
    {
        public string PropertyType { get; set; }
        public IList<MapItem> MatchItem { get; set; }

        public class MapItem
        {
            public string CapturedText{ get; set;}
            public object Obj { get; set; }
            public bool ParseFailed { get; set; }
            public override string ToString()
            {
                if (ParseFailed) { return string.Format("Unable to parse '{0}'", CapturedText); }
                if (Obj == null) { return "-null-"; }
                return Obj.ToString();
            }
        }
        public override string ToString()
        {
            string mapItems;
            if (MatchItem.Count == 0) { return string.Empty;  }
            if (MatchItem.Count == 1) { mapItems = MatchItem[0].ToString(); }
            else
            {
                var sb = new StringBuilder();
                for (var i = 0; i < MatchItem.Count; i++)
                {
                    sb.AppendFormat("[{0}]{1},", i, MatchItem[i]);
                }
                sb.Length = sb.Length - 1;
                mapItems = sb.ToString();
            }
            return string.Format("<{0}> {{{1}}}", PropertyType,mapItems);

        }
    }
}
