namespace JournalClub
{
    using System;
    using System.Data.Entity;
    public class JournalClubContextSeedInitializer : CreateDatabaseIfNotExists<JournalClubContext>
    {
        protected override void Seed(JournalClub.JournalClubContext context)
        {
            context.Articles.Add(new Article
                {
                    Id = 1,
                    Title = "Hydroxyethyl Starch or Saline for Fluid Resuscitation in Intensive Care",
                    Journal = "N Engl J Med",
                    YearPublished = 2012,
                    Authors = "John A. Myburgh, Simon Finfer, Rinaldo Bellomo et al.",
                    ArticleTypeId = 1,
                    PowerPointLocation = null,
                    PresentationId = null,
                    ArticleLocation = null,
                    Reference = "367:1901-1911",
                    PMID = null
                });
            context.Articles.Add(new Article
                {
                    Id = 2,
                    Title = "Initial trophic vs full enteral feeding in patients with acute lung injury: the EDEN randomized trial.",
                    Journal = "JAMA",
                    YearPublished = 2012,
                    Authors = "The National Heart, Lung, and Blood Institute Acute Respiratory",
                    ArticleTypeId = 1,
                    PowerPointLocation = null,
                    PresentationId = null,
                    ArticleLocation = null,
                    Reference = "307: 795–803.",
                    PMID = null
                });
            context.Articles.Add(new Article
                {
                    Id = 3,
                    Title = "Intramuscular versus intravenous therapy for prehospital status epilepticus.",
                    Journal = "N Engl J Med",
                    YearPublished = 2012,
                    Authors = "Silbergleit R, Durkalski V, Lowenstein D et al.",
                    ArticleTypeId = 1,
                    PowerPointLocation = null,
                    PresentationId = null,
                    ArticleLocation = null,
                    Reference = "366:591-0–600.",
                    PMID = null
                });
            context.Articles.Add(new Article
                {
                    Id = 4,
                    Title = "Association of body temperature and antipyretic treatments with mortality of critically ill patients with and without sepsis: multi-centered prospective observational",
                    Journal = "Crit Care",
                    YearPublished = 2012,
                    Authors = "Egi M, Kim JY, Suh GY, Koh Y, Nishimura M.",
                    ArticleTypeId = 1,
                    PowerPointLocation = null,
                    PresentationId = new Guid("1ab92b36-a078-49dc-a54e-0fc38302f98a"),
                    ArticleLocation = null,
                    Reference = "16: R33.",
                    PMID = null
                });
            context.Articles.Add(new Article
                {
                    Id = 5,
                    Title = "An observational study fluid balance and patient outcomes in the randomized evaluation of normal vs. augmented level of replacement therapy trial.",
                    Journal = "Crit Care Med",
                    YearPublished = 2012,
                    Authors = "The RENAL Replacement Therapy Study Investigators.",
                    ArticleTypeId = 2,
                    PowerPointLocation = null,
                    PresentationId = null,
                    ArticleLocation = null,
                    Reference = "40:1753–1760.",
                    PMID = null
                });
            context.Articles.Add(new Article
                {
                    Id = 6,
                    Title = "Mortality after fluid bolus in children with shock due to sepsis or severe infection:",
                    Journal = "PLOS One",
                    YearPublished = 2012,
                    Authors = "Ford N, Hargreaves S, Shanks L.",
                    ArticleTypeId = 3,
                    PowerPointLocation = null,
                    PresentationId = null,
                    ArticleLocation = null,
                    Reference = "7:e43953.",
                    PMID = null
                });
            context.Articles.Add(new Article
                {
                    Id = 7,
                    Title = "Association between valvular surgery and mortality among patients with infective endocarditis complicated by heart failure.",
                    Journal = "JAMA",
                    YearPublished = 2011,
                    Authors = "Kiefer T, Park L, Tribouilloy C et al.",
                    ArticleTypeId = 5,
                    PowerPointLocation = null,
                    PresentationId = null,
                    ArticleLocation = null,
                    Reference = "306:2239–2247.",
                    PMID = null
                });
            context.Articles.Add(new Article
                {
                    Id = 8,
                    Title = "Effect of tranexamic acid on mortality in patients with traumatic bleeding: prespecified analysis of data from randomised controlled trial.",
                    Journal = "BMJ",
                    YearPublished = 2012,
                    Authors = "Roberts I, Perel P, Prieto-Merino D et al.",
                    ArticleTypeId = 1,
                    PowerPointLocation = null,
                    PresentationId = new Guid("ec15a908-6f77-4264-b5c0-ac0d381fac3f"),
                    ArticleLocation = null,
                    Reference = "345: e5839",
                    PMID = null
                });
            context.Articles.Add(new Article
                {
                    Id = 9,
                    Title = "Old agent, new experience: colistin use in the paediatric intensive care unit.",
                    Journal = "Int J Antimicrob Agents",
                    YearPublished = 2012,
                    Authors = "Paksu MS, Paksu S, Karadag A et al.",
                    ArticleTypeId = 2,
                    PowerPointLocation = null,
                    PresentationId = new Guid("a45d1247-6025-4026-aa6a-afeb89c2a772"),
                    ArticleLocation = null,
                    Reference = "40:140-144.",
                    PMID = null
                });
            context.Articles.Add(new Article
                {
                    Id = 10,
                    Title = "What’s new in resuscitation strategies for the patient with multiple trauma?",
                    Journal = "Injury",
                    YearPublished = 2012,
                    Authors = "Curry N, Davis PW. ",
                    ArticleTypeId = 4,
                    PowerPointLocation = null,
                    PresentationId = new Guid("41de83a1-fdf7-4b8e-b573-df5d39a0d134"),
                    ArticleLocation = null,
                    Reference = "43:1021-1028",
                    PMID = null
                });
            context.Articles.Add(new Article
                {
                    Id = 11,
                    Title = "Drotrecogin alfa (activated) in adults with septic shock.",
                    Journal = "N Engl J Med",
                    YearPublished = 2012,
                    Authors = "Ranieri VM, Thompson BT, Barie PS et al.",
                    ArticleTypeId = 1,
                    PowerPointLocation = null,
                    PresentationId = new Guid("6f715334-da5b-43ee-825a-32f5c3b6cb2f"),
                    ArticleLocation = null,
                    Reference = "366:2055-2064.",
                    PMID = null
                });
            context.Articles.Add(new Article
                {
                    Id = 12,
                    Title = "Transfusion of packed red blood cells is not associated with improved central venous oxygen saturation or organ function in patients with septic shock.",
                    Journal = "J Emerg Med",
                    YearPublished = 2012,
                    Authors = "Fuller BM, Gajera M, Schorr C et al.",
                    ArticleTypeId = 5,
                    PowerPointLocation = null,
                    PresentationId = null,
                    ArticleLocation = null,
                    Reference = " 43: 593–598.",
                    PMID = null
                });
            context.Articles.Add(new Article
                {
                    Id = 13,
                    Title = "Neurocognitive development of children 4 years after critical illness and treatment with tight glucose control: a randomized controlled trial.",
                    Journal = "JAMA",
                    YearPublished = 2012,
                    Authors = "Mesotten D, Gielen M, Sterken C et al.",
                    ArticleTypeId = 1,
                    PowerPointLocation = null,
                    PresentationId = null,
                    ArticleLocation = null,
                    Reference = "308(16)",
                    PMID = null
                });
            context.Articles.Add(new Article
                {
                    Id = 14,
                    Title = "Zinc as adjunct treatment in infants aged between 7 and 120 days with probable serious bacterial infection: a randomised, double-blind, placebo-controlled trial.",
                    Journal = "Lancet",
                    YearPublished = 2012,
                    Authors = "Bhatnagar S, Wadhwa N, Aneja S et al.",
                    ArticleTypeId = 1,
                    PowerPointLocation = null,
                    PresentationId = null,
                    ArticleLocation = null,
                    Reference = " 379: 2072–2078.",
                    PMID = null
                });
            context.Articles.Add(new Article
                {
                    Id = 15,
                    Title = "The risk of catheter-related bloodstream infection with femoral venous catheters as compared to subclavian and internal jugular venous catheters: a systematic review of the literature and metaanalysis.",
                    Journal = "Crit Care Med",
                    YearPublished = 2012,
                    Authors = "Marik PE, Flemmer M, Harrison W.",
                    ArticleTypeId = 3,
                    PowerPointLocation = null,
                    PresentationId = null,
                    ArticleLocation = null,
                    Reference = "40:2479–2485.",
                    PMID = null
                });
            context.Articles.Add(new Article
                {
                    Id = 16,
                    Title = "Impact on hospital mortality of catheter removal and adequate antifungal therapy in Candida spp. bloodstream infections.",
                    Journal = "J Antimicrob Chemother",
                    YearPublished = 2013,
                    Authors = "Garnacho-Montero J, Díaz-Martín A, García-Cabrera E, Ruiz Pérez de Pipaón M, Hernández-Caballero C, Lepe-Jiménez JA.",
                    ArticleTypeId = 2,
                    PowerPointLocation = null,
                    PresentationId = null,
                    ArticleLocation = null,
                    Reference = "68:206-213.",
                    PMID = null
                });
            context.ArticleTypes.Add(new ArticleType
                {
                    Id = 1,
                    Description = "Randomised Control Trial"
                });
            context.ArticleTypes.Add(new ArticleType
                {
                    Id = 2,
                    Description = "Prospective Observational Study"
                });
            context.ArticleTypes.Add(new ArticleType
                {
                    Id = 3,
                    Description = "Meta Analysis"
                });
            context.ArticleTypes.Add(new ArticleType
                {
                    Id = 4,
                    Description = "Review Article"
                });
            context.ArticleTypes.Add(new ArticleType
                {
                    Id = 5,
                    Description = "Retrospective Observational Study"
                });
            context.JournalReferenceRegExps.Add(new JournalReferenceRegExp
                {
                    Id = 1,
                    Regex = "^(?<Authors>[\\w ,]+)\\W+(?<Title>[^.]+)[. ]*(?<Journal>\\D+)(?<YearPublished>\\d+).*;(?<Reference>[^.]+)(.*PMID: *(?<PMID>\\d+))?",
                    ExpName = "National Library Medicine (NLM) Author 1st",
                    SingleLine = false
                });
            context.JournalReferenceRegExps.Add(new JournalReferenceRegExp
                {
                    Id = 2,
                    Regex = "^(?<Title>[^.]+)[\\W]*(?<Authors>[\\w ,]+)\\W+(?<Journal>\\D+)(?<YearPublished>\\d+).*;(?<Reference>[^.]+)(.*\\n*PMID: *(?<PMID>\\d+))?",
                    ExpName = "National Library Medicine (NLM) Title 1st",
                    SingleLine = false
                });
            context.JournalReferenceRegExps.Add(new JournalReferenceRegExp
                {
                    Id = 3,
                    Regex = "(?<Title>.*?)ICM Index Number(.*?)Authors: (?<Authors>.*?)\\r\\nReference: (?<Journal>[^\\d]+)(?<YearPublished>\\d{4});(?<Reference>.*)",
                    ExpName = "Intensive Care Monitor",
                    SingleLine = true
                });
            context.PicuAttachments.Add(new PicuAttachment
                {
                    Id = new Guid("088ca2fc-5f48-43db-a5da-9b377e0fdd48"),
                    PresenterId = new Guid("15781c7c-ea64-4c64-8385-55f9dbe03280"),
                    StartDate = new DateTime(2012,12,10,0,0,0,0),
                    FinishDate = new DateTime(2013,6,10,0,0,0,0)
                });
            context.PicuAttachments.Add(new PicuAttachment
                {
                    Id = new Guid("3fe3a750-83ad-4d99-a382-24a484e41a29"),
                    PresenterId = new Guid("2e960c6b-be18-4be8-8dcb-833a0ff1a908"),
                    StartDate = new DateTime(2012,12,10,0,0,0,0),
                    FinishDate = new DateTime(2013,6,9,0,0,0,0)
                });
            context.PicuAttachments.Add(new PicuAttachment
                {
                    Id = new Guid("16727947-b407-461a-b7ac-a3e546986a96"),
                    PresenterId = new Guid("39798c83-3dde-48a1-85a5-9eb496bc43bf"),
                    StartDate = new DateTime(2012,12,10,0,0,0,0),
                    FinishDate = new DateTime(2013,6,10,0,0,0,0)
                });
            context.PicuAttachments.Add(new PicuAttachment
                {
                    Id = new Guid("a7eca4ce-d0ba-4985-a356-97221750a210"),
                    PresenterId = new Guid("3ff4b656-5928-4b7c-bb7a-6dff35deae6d"),
                    StartDate = new DateTime(2009,12,10,0,0,0,0),
                    FinishDate = null
                });
            context.PicuAttachments.Add(new PicuAttachment
                {
                    Id = new Guid("e3d104b4-f96f-4512-a910-ef4f16660267"),
                    PresenterId = new Guid("407477a9-3fae-4968-b03f-086d84b6bf2e"),
                    StartDate = new DateTime(2012,12,10,0,0,0,0),
                    FinishDate = new DateTime(2013,6,10,0,0,0,0)
                });
            context.PicuAttachments.Add(new PicuAttachment
                {
                    Id = new Guid("e85e1b9c-22fe-4df7-b6c9-15ddb70f05eb"),
                    PresenterId = new Guid("5b0d6f24-9a09-4c10-837c-5ea30579a410"),
                    StartDate = new DateTime(2012,12,10,0,0,0,0),
                    FinishDate = new DateTime(2013,6,9,0,0,0,0)
                });
            context.PicuAttachments.Add(new PicuAttachment
                {
                    Id = new Guid("808c2023-4269-4812-bb83-4fec379b7a5a"),
                    PresenterId = new Guid("9b568124-a9a5-47b5-995b-6fe451eab37a"),
                    StartDate = new DateTime(2012,12,10,0,0,0,0),
                    FinishDate = new DateTime(2013,6,10,0,0,0,0)
                });
            context.PicuAttachments.Add(new PicuAttachment
                {
                    Id = new Guid("a6dcbe4a-1c13-4f1f-a640-8ccd1653ce27"),
                    PresenterId = new Guid("a4756ab8-3879-4858-9b74-f745a64f0a5f"),
                    StartDate = new DateTime(2012,12,10,0,0,0,0),
                    FinishDate = new DateTime(2013,6,10,0,0,0,0)
                });
            context.PicuAttachments.Add(new PicuAttachment
                {
                    Id = new Guid("1559e71e-7f64-4599-ab6b-b7313fbaad22"),
                    PresenterId = new Guid("a60a4b0f-54cf-4ec6-ad2c-7b8d65f584c5"),
                    StartDate = new DateTime(2012,12,10,0,0,0,0),
                    FinishDate = new DateTime(2013,6,10,0,0,0,0)
                });
            context.PicuAttachments.Add(new PicuAttachment
                {
                    Id = new Guid("c0ea4387-aa33-443e-a03c-c12917e62141"),
                    PresenterId = new Guid("b070fc26-7830-4b9c-9f41-eeb3b410eafe"),
                    StartDate = new DateTime(2012,12,10,0,0,0,0),
                    FinishDate = new DateTime(2013,6,10,0,0,0,0)
                });
            context.PicuAttachments.Add(new PicuAttachment
                {
                    Id = new Guid("d6adabb2-a601-42a8-900d-0714fd0b0c3f"),
                    PresenterId = new Guid("b3568d8d-db53-4ce8-bfe6-10ba34e99ef3"),
                    StartDate = new DateTime(2012,12,10,0,0,0,0),
                    FinishDate = new DateTime(2013,6,10,0,0,0,0)
                });
            context.PicuAttachments.Add(new PicuAttachment
                {
                    Id = new Guid("bfd51fb5-9a0e-4e56-9db5-f2374eec2550"),
                    PresenterId = new Guid("cba40f80-fed0-44db-a26e-d6e47c4059cf"),
                    StartDate = new DateTime(2012,12,10,0,0,0,0),
                    FinishDate = new DateTime(2013,4,7,0,0,0,0)
                });
            context.PicuAttachments.Add(new PicuAttachment
                {
                    Id = new Guid("2f7f0a42-05df-44ef-a8ef-5bd43239b88c"),
                    PresenterId = new Guid("f4f85463-ef22-4bd1-93af-a823c4c43511"),
                    StartDate = new DateTime(2012,12,10,0,0,0,0),
                    FinishDate = new DateTime(2013,6,10,0,0,0,0)
                });
            context.Presentations.Add(new Presentation
                {
                    Id = new Guid("200f26bb-b23d-426d-a4b5-aec43b4ed556"),
                    PresenterId = new Guid("15781c7c-ea64-4c64-8385-55f9dbe03280"),
                    TeachingSessionId = null
                });
            context.Presentations.Add(new Presentation
                {
                    Id = new Guid("032d40c7-c108-4184-9801-7565c2ad2fa6"),
                    PresenterId = new Guid("b070fc26-7830-4b9c-9f41-eeb3b410eafe"),
                    TeachingSessionId = null
                });
            context.Presentations.Add(new Presentation
                {
                    Id = new Guid("ec15a908-6f77-4264-b5c0-ac0d381fac3f"),
                    PresenterId = new Guid("cba40f80-fed0-44db-a26e-d6e47c4059cf"),
                    TeachingSessionId = null
                });
            context.Presentations.Add(new Presentation
                {
                    Id = new Guid("a45d1247-6025-4026-aa6a-afeb89c2a772"),
                    PresenterId = new Guid("f4f85463-ef22-4bd1-93af-a823c4c43511"),
                    TeachingSessionId = null
                });
            context.Presentations.Add(new Presentation
                {
                    Id = new Guid("1ab92b36-a078-49dc-a54e-0fc38302f98a"),
                    PresenterId = new Guid("3ff4b656-5928-4b7c-bb7a-6dff35deae6d"),
                    TeachingSessionId = new Guid("375f5624-b1ac-4474-a77c-6a936a88956d")
                });
            context.Presentations.Add(new Presentation
                {
                    Id = new Guid("41de83a1-fdf7-4b8e-b573-df5d39a0d134"),
                    PresenterId = new Guid("a4756ab8-3879-4858-9b74-f745a64f0a5f"),
                    TeachingSessionId = new Guid("57af5f4e-8b21-4f54-a38d-8a90bf863bb9")
                });
            context.Presentations.Add(new Presentation
                {
                    Id = new Guid("9a4b7bc3-0755-41d4-bdf1-7ebb09511e4e"),
                    PresenterId = new Guid("39798c83-3dde-48a1-85a5-9eb496bc43bf"),
                    TeachingSessionId = new Guid("5ebd75d0-e8b7-4af4-bd87-88521c315288")
                });
            context.Presentations.Add(new Presentation
                {
                    Id = new Guid("6f715334-da5b-43ee-825a-32f5c3b6cb2f"),
                    PresenterId = new Guid("9b568124-a9a5-47b5-995b-6fe451eab37a"),
                    TeachingSessionId = new Guid("9302ee27-f769-469e-b8c2-ac9782e4a5a9")
                });
            context.Presentations.Add(new Presentation
                {
                    Id = new Guid("fdd259a3-a7a9-453a-b8d1-6dc58b9ce0a8"),
                    PresenterId = new Guid("b3568d8d-db53-4ce8-bfe6-10ba34e99ef3"),
                    TeachingSessionId = new Guid("c5d4d11a-7be8-4c8e-82d4-2bccb4bf0c3a")
                });
            context.Presentations.Add(new Presentation
                {
                    Id = new Guid("ad4dcf36-9ce2-4893-99ab-c2f6cef625c2"),
                    PresenterId = new Guid("407477a9-3fae-4968-b03f-086d84b6bf2e"),
                    TeachingSessionId = new Guid("c95170ee-aed9-40f6-b6a3-360100641a16")
                });
            context.Presentations.Add(new Presentation
                {
                    Id = new Guid("08748f63-cfd9-4156-9c47-666182f80c9f"),
                    PresenterId = new Guid("5b0d6f24-9a09-4c10-837c-5ea30579a410"),
                    TeachingSessionId = new Guid("d29eba9c-7e48-4da3-8ed3-332fab84b10e")
                });
            context.Presentations.Add(new Presentation
                {
                    Id = new Guid("e08bc81d-d755-4bce-b1ce-1c1a0b24875e"),
                    PresenterId = new Guid("2e960c6b-be18-4be8-8dcb-833a0ff1a908"),
                    TeachingSessionId = new Guid("dacf40b0-c000-4624-8865-210bf9ccc80d")
                });
            context.Presentations.Add(new Presentation
                {
                    Id = new Guid("c0f43daf-3e80-4e40-87af-3681c3749a6a"),
                    PresenterId = new Guid("a60a4b0f-54cf-4ec6-ad2c-7b8d65f584c5"),
                    TeachingSessionId = new Guid("e54d3414-6a92-44d8-8394-721444e498de")
                });
            context.Presenters.Add(new Presenter
                {
                    Id = new Guid("15781c7c-ea64-4c64-8385-55f9dbe03280"),
                    WorkEmail = null,
                    IsRegistrar = true,
                    FullName = "Catherine Webb"
                });
            context.Presenters.Add(new Presenter
                {
                    Id = new Guid("2e960c6b-be18-4be8-8dcb-833a0ff1a908"),
                    WorkEmail = null,
                    IsRegistrar = false,
                    FullName = "Sylvia Seibt"
                });
            context.Presenters.Add(new Presenter
                {
                    Id = new Guid("39798c83-3dde-48a1-85a5-9eb496bc43bf"),
                    WorkEmail = null,
                    IsRegistrar = true,
                    FullName = "Jascha Kehr"
                });
            context.Presenters.Add(new Presenter
                {
                    Id = new Guid("3ff4b656-5928-4b7c-bb7a-6dff35deae6d"),
                    WorkEmail = null,
                    IsRegistrar = false,
                    FullName = "Brent McSharry"
                });
            context.Presenters.Add(new Presenter
                {
                    Id = new Guid("407477a9-3fae-4968-b03f-086d84b6bf2e"),
                    WorkEmail = null,
                    IsRegistrar = true,
                    FullName = "Richard Newton"
                });
            context.Presenters.Add(new Presenter
                {
                    Id = new Guid("5b0d6f24-9a09-4c10-837c-5ea30579a410"),
                    WorkEmail = null,
                    IsRegistrar = false,
                    FullName = "Tom Winter"
                });
            context.Presenters.Add(new Presenter
                {
                    Id = new Guid("9b568124-a9a5-47b5-995b-6fe451eab37a"),
                    WorkEmail = null,
                    IsRegistrar = true,
                    FullName = "Michael Herd"
                });
            context.Presenters.Add(new Presenter
                {
                    Id = new Guid("a4756ab8-3879-4858-9b74-f745a64f0a5f"),
                    WorkEmail = null,
                    IsRegistrar = true,
                    FullName = "Finn Coulter"
                });
            context.Presenters.Add(new Presenter
                {
                    Id = new Guid("a60a4b0f-54cf-4ec6-ad2c-7b8d65f584c5"),
                    WorkEmail = null,
                    IsRegistrar = true,
                    FullName = "Emma Taylor"
                });
            context.Presenters.Add(new Presenter
                {
                    Id = new Guid("b070fc26-7830-4b9c-9f41-eeb3b410eafe"),
                    WorkEmail = null,
                    IsRegistrar = true,
                    FullName = "Bhavni Shah"
                });
            context.Presenters.Add(new Presenter
                {
                    Id = new Guid("b3568d8d-db53-4ce8-bfe6-10ba34e99ef3"),
                    WorkEmail = null,
                    IsRegistrar = true,
                    FullName = "Katie Moynihan"
                });
            context.Presenters.Add(new Presenter
                {
                    Id = new Guid("cba40f80-fed0-44db-a26e-d6e47c4059cf"),
                    WorkEmail = null,
                    IsRegistrar = true,
                    FullName = "Michael Tan"
                });
            context.Presenters.Add(new Presenter
                {
                    Id = new Guid("f4f85463-ef22-4bd1-93af-a823c4c43511"),
                    WorkEmail = null,
                    IsRegistrar = true,
                    FullName = "Elspeth Richardson"
                });
            context.TeachingSessions.Add(new TeachingSession
                {
                    Id = new Guid("0c7c2654-4fe1-450e-80fb-4cb94b7f7bcf"),
                    SessionDate = new DateTime(2013,6,12,0,0,0,0)
                });
            context.TeachingSessions.Add(new TeachingSession
                {
                    Id = new Guid("375f5624-b1ac-4474-a77c-6a936a88956d"),
                    SessionDate = new DateTime(2013,2,13,0,0,0,0)
                });
            context.TeachingSessions.Add(new TeachingSession
                {
                    Id = new Guid("48e72844-3bd1-411d-9ba5-44821bd4bea4"),
                    SessionDate = new DateTime(2013,7,10,0,0,0,0)
                });
            context.TeachingSessions.Add(new TeachingSession
                {
                    Id = new Guid("57af5f4e-8b21-4f54-a38d-8a90bf863bb9"),
                    SessionDate = new DateTime(2013,4,24,0,0,0,0)
                });
            context.TeachingSessions.Add(new TeachingSession
                {
                    Id = new Guid("5ebd75d0-e8b7-4af4-bd87-88521c315288"),
                    SessionDate = new DateTime(2013,6,5,0,0,0,0)
                });
            context.TeachingSessions.Add(new TeachingSession
                {
                    Id = new Guid("873c4632-1dbd-4ed1-b9e1-fde575a10364"),
                    SessionDate = new DateTime(2013,6,19,0,0,0,0)
                });
            context.TeachingSessions.Add(new TeachingSession
                {
                    Id = new Guid("9302ee27-f769-469e-b8c2-ac9782e4a5a9"),
                    SessionDate = new DateTime(2013,5,1,0,0,0,0)
                });
            context.TeachingSessions.Add(new TeachingSession
                {
                    Id = new Guid("c5d4d11a-7be8-4c8e-82d4-2bccb4bf0c3a"),
                    SessionDate = new DateTime(2013,3,27,0,0,0,0)
                });
            context.TeachingSessions.Add(new TeachingSession
                {
                    Id = new Guid("c6ed02f4-e4d3-4332-a00f-fbdc12b4b3db"),
                    SessionDate = new DateTime(2013,6,26,0,0,0,0)
                });
            context.TeachingSessions.Add(new TeachingSession
                {
                    Id = new Guid("c95170ee-aed9-40f6-b6a3-360100641a16"),
                    SessionDate = new DateTime(2013,4,3,0,0,0,0)
                });
            context.TeachingSessions.Add(new TeachingSession
                {
                    Id = new Guid("d29eba9c-7e48-4da3-8ed3-332fab84b10e"),
                    SessionDate = new DateTime(2013,3,6,0,0,0,0)
                });
            context.TeachingSessions.Add(new TeachingSession
                {
                    Id = new Guid("dacf40b0-c000-4624-8865-210bf9ccc80d"),
                    SessionDate = new DateTime(2013,5,8,0,0,0,0)
                });
            context.TeachingSessions.Add(new TeachingSession
                {
                    Id = new Guid("e54d3414-6a92-44d8-8394-721444e498de"),
                    SessionDate = new DateTime(2013,5,15,0,0,0,0)
                });

            context.SaveChanges();
        }
    }
}