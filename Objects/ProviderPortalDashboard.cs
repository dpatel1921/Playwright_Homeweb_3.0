using Microsoft.Playwright;

namespace ProviderPortalSmokeTest.Objects
{
    public class ProviderPortalDashboard
    {
        private readonly IPage _page;

        // Constructor to pass the Playwright page context into the POM
        public ProviderPortalDashboard(IPage page)
        {
            _page = page;
        }

        // --- AUTHENTICATION & LOGIN ---
        public ILocator ProviderName => _page.Locator("//INPUT[@id='Email']");
        public ILocator ProviderPassword => _page.Locator("//INPUT[@id='providerpasswordField']");
        public ILocator LoginButton => _page.Locator(".rz-button-box");
        public ILocator Login => _page.Locator("//BUTTON[@class='btn btn-primary']");
        public ILocator RadioSelect => _page.Locator("//LABEL[@class='form-check-label'][text()=' Security question']");
        public ILocator SecurityAnswer => _page.Locator("//INPUT[@id='securityAnswer']");
        public ILocator SecurityLogin => _page.Locator("//BUTTON[@id='btSendCode']");
        public ILocator Button => _page.Locator("button#btnLoginSecurityQestion");
        public ILocator Placeholder => _page.Locator("/html/body/form/div[3]/div[1]/div[1]/div/div/fieldset/p[6]/b/a");

        // --- DASHBOARD NAVIGATION ---
        public ILocator Pendingcases => _page.Locator("//SPAN[@class='rz-link-text'][contains (text(),'Cases Pending Acceptance')]");
        public ILocator ServiceEvent => _page.Locator("//span[normalize-space()='Service/Event Search']");
        public ILocator AptConfirm => _page.Locator("//span[normalize-space()='Cases Awaiting First Appointment Confirmation']");
        public ILocator AptConfirm1 => _page.Locator("//SPAN[@class='rz-button-text'][text()='Confirm 1st Appt. Date']");
        public ILocator AptConfirm2 => _page.Locator("//SPAN[@class='rz-button-text'][text()='Save']");
        public ILocator UpdateContactInfo => _page.Locator("//span[normalize-space()='Update Contact Info']");
        public ILocator ViewInvoice => _page.Locator("//span[normalize-space()='View My Invoice']");
        public ILocator ViewDoc => _page.Locator("//span[normalize-space()='Documents - Information']");
        public ILocator ViewDoc1 => _page.Locator("//span[normalize-space()='Releasing Client Information']");
        public ILocator ViewDoc2 => _page.Locator("//span[normalize-space()='General Resources & Training Opportunities']");
        public ILocator ViewDoc3 => _page.Locator("//span[normalize-space()='Counselling Services']");
        public ILocator ViewDoc4 => _page.Locator("//span[normalize-space()='Enhanced Mental Health Care']");
        public ILocator ViewDoc5 => _page.Locator("//span[normalize-space()='Crisis Management Services']");
        public ILocator ViewDoc6 => _page.Locator("//span[normalize-space()='LifeSmart']");

        public ILocator Support0 => _page.Locator("//span[normalize-space()='Health Promotions']");
        public ILocator Support1 => _page.Locator("//span[normalize-space()='Workplace Interventions']");
        public ILocator Support2 => _page.Locator("//span[normalize-space()='Complex Care & Specialty Services']");
        public ILocator Support3 => _page.Locator("//a[@title='Support']");
        public ILocator Support4 => _page.Locator("//span[@class='rz-tabview-title']");

        // --- MESSAGE CENTRE ---
        public ILocator MessageCentre => _page.Locator("//span[normalize-space()='Message Centre Archive']");
        public ILocator MessageCentre1 => _page.Locator("//table[contains(@class,'rz-grid-table rz-grid-table-fixed')]");
        public ILocator MessageCentre2 => _page.Locator("//SPAN[@class='rz-button-text'][text()='Close']");
        public ILocator MessageCentre3 => _page.Locator("(//SPAN[@class='rz-button-text'][text()='Delete'])[1]");

        // --- CONTACTS ---
        public ILocator Addnewcontact => _page.Locator("//SPAN[@class='rz-button-text'][text()='Add New']");
        public ILocator ViewInvoice1 => _page.Locator("(//span[@class='rz-dropdown-label rz-inputtext'])[2]");
        public ILocator ViewInvoice2 => _page.Locator("//li[contains(@aria-label,'March 2025 - 257933')]");
        public ILocator ViewInvoice3 => _page.Locator("//SPAN[@class='rz-button-box']");
        public ILocator ViewInvoice4 => _page.Locator("//span[contains(@class,'notranslate rzi')]");
        public ILocator Addnewcontactselect => _page.Locator("//table[contains(@class,'rz-grid-table rz-grid-table-fixed')]");
        public ILocator Addnewcontactphone => _page.Locator("//li[contains(@aria-label,'Home_Phone')]//span[1]");
        public ILocator Addnewcontactvalue => _page.Locator("//INPUT[@id='DisplayValue']");
        public ILocator Addnewcontactcheckbox => _page.Locator("//i[@class='notranslate rz-button-icon-left rzi'][normalize-space()='check']");
        public ILocator Addnewcontactsave => _page.Locator("//i[normalize-space()='check']");
        public ILocator Addnewcontactedit => _page.Locator("//table[contains(@class,'rz-grid-table rz-grid-table-fixed')]/tbody[1]/tr[1]/td[5]/span[1]/button[1]");
        public ILocator Addnewcontactdelete => _page.Locator("(//I[@class='rz-button-icon-left rzi'])[3]");
        public ILocator Close => _page.Locator("//SPAN[@class='rz-button-text'][text()='Close']");

        // --- CASE MANAGEMENT ---
        public ILocator Casesawaiting => _page.Locator("//SPAN[@class='rz-link-text'][contains (text(),'Cases Awaiting First Appointment Confirmation ')]");
        public ILocator Requestcaseext => _page.Locator("//SPAN[@class='rz-link-text'][contains (text(),'Request for Case Extension')]");
        public ILocator PendingDraft => _page.Locator("//SPAN[@class='rz-link-text'][contains (text(),'Pending(Draft)')]");
        public ILocator OpenCases => _page.Locator("//SPAN[@class='rz-link-text'][contains (text(),'My open cases')]");
        public ILocator Ecounseling => _page.Locator("//SPAN[@class='rz-link-text'][contains (text(), 'Open E-Counselling cases with pending messages/upcoming appointments')]");
        public ILocator Schedules => _page.Locator("//SPAN[@class='rz-navigation-item-text'][text()='Schedules']");
        public ILocator Calendar => _page.Locator("//SPAN[@class='rz-navigation-item-text'][text()='My Calendar']");
        public ILocator Myopencases => _page.Locator("(//SPAN[@class='rz-navigation-item-text'][text()='My open cases'])[2]");
        public ILocator Contactinfo => _page.Locator("//SPAN[@class='rz-navigation-item-text'][text()='Update Contact Info']");
        public ILocator Messages => _page.Locator("//SPAN[@class='rz-navigation-item-text'][contains (text(), 'Message Board')]");
        public ILocator Profile => _page.Locator("//SPAN[@class='rz-navigation-item-text'][text()='View My Profile']");
        public ILocator Documents => _page.Locator("//SPAN[@class='rz-navigation-item-text'][text()='Documents - Information']");
        public ILocator Invoice => _page.Locator("//SPAN[@class='rz-navigation-item-text'][text()='View My Invoice']");
        public ILocator Support => _page.Locator("//SPAN[@class='rz-navigation-item-text'][text()='Support Links']");
        public ILocator Archive => _page.Locator("//SPAN[@class='rz-navigation-item-text'][text()='Message Centre Archive']");
        public ILocator Logout => _page.Locator("//BUTTON[@class='btn btn-nav-item btn-nav-search btn-icon-spaced'][text()='Log out']");
        public ILocator Servicesearch => _page.Locator("//input[@placeholder='Case ID']");
        public ILocator Searchclick => _page.Locator("(//SPAN[@class='rz-button-box'])[1]");
        public ILocator OpenCase => _page.Locator("(//SPAN[@class='rz-button-box'])[3]");

        // --- APPOINTMENTS & BOOKING ---
        public ILocator AddForm => _page.Locator("//SPAN[@class='rz-dropdown-label rz-inputtext rz-placeholder']");
        public ILocator Aptbooking => _page.Locator("//A[@href=''][text()='Go to Calendar']");
        public ILocator Aptbooking1 => _page.Locator("(//SPAN[@class='rz-button-text'][text()='Book'])[1]");
        public ILocator Aptbooking2 => _page.Locator("(//div[contains(@class,'rz-dropdown valid')])[2]");
        public ILocator Aptbooking3 => _page.Locator("//span[normalize-space()='phone']");
        public ILocator Aptbooking4 => _page.Locator("(//SPAN[@class='rz-radiobutton-label'])[3]");
        public ILocator Aptbooking5 => _page.Locator("//button[@type='submit']");
        public ILocator Aptbooking6 => _page.Locator("//SPAN[@class='rz-button-text'][text()='DETAILS Confirmed with Client']");

        public ILocator Profile1 => _page.Locator("(//button[contains(@class,'rz-button rz-button-md')])[2]");
        public ILocator Profile2 => _page.Locator("//SPAN[@class='rz-tabview-title'][text()='Contact Device']");
        public ILocator Profile3 => _page.Locator("//SPAN[@class='rz-tabview-title'][text()='Address']");
        public ILocator Profile4 => _page.Locator("//SPAN[@class='rz-button-text'][text()='Close']");
        public ILocator Filter => _page.Locator("//span[normalize-space()='Select...']");
        public ILocator Filter1 => _page.Locator("//span[normalize-space()='Monday']");
        public ILocator Filter2 => _page.Locator("//SPAN[@class='rz-button-text'][text()='Apply Filter']");
        public ILocator Hold => _page.Locator("(//SPAN[@class='rz-button-text'][text()='Hold Appt'])[1]");
        public ILocator Hold1 => _page.Locator("//SPAN[@class='rz-button-text'][text()='Hold']");
        public ILocator Hold2 => _page.Locator("//SPAN[@class='rz-button-text'][text()='Release']");
        public ILocator Rebooking => _page.Locator("//span[normalize-space()='Rebook Or Cancel Appointment']");
        public ILocator Rebooking1 => _page.Locator("(//SPAN[@class='rz-button-text'][text()='Cancel'])[3]");
        public ILocator Rebooking2 => _page.Locator("//SPAN[@class='rz-button-text'][text()='Cancel Appointment']");
        public ILocator Rebooking3 => _page.Locator("//span[normalize-space(text())='Resend email/sms']");
        public ILocator Rebooking4 => _page.Locator("//SPAN[@class='rz-button-text'][text()='Send Email']");

        // --- DOCUMENTS & EVENTS ---
        public ILocator AddDocument => _page.Locator("//SPAN[@class='rz-button-text'][text()='Add Document']");
        public ILocator FiletoUpload => _page.Locator("(//label[normalize-space(text())='Notes']/following::input)[2]");
        public ILocator SaveFile => _page.Locator("//span[normalize-space(text())='Save']");
        public ILocator EMHC => _page.Locator("//li[@aria-label='EMHC First Session Note & Billing form']//span[1]");
        public ILocator AddEMHC => _page.Locator("//SPAN[@class='rz-button-text'][text()='Add Event']");
        public ILocator Datetime => _page.Locator("(//SPAN[@aria-hidden='true'])[1]");
        public ILocator Selectoffice => _page.Locator("(//SPAN[@class='rz-dropdown-label rz-inputtext rz-placeholder'])[1]");
        public ILocator SendDate => _page.Locator("//INPUT[@id='dpSessionDate']");
        public ILocator SendTime => _page.Locator("(//I[@class='rz-button-icon-left rzi'])[15]");
        public ILocator SendTimeEMHC => _page.Locator("(//I[@class='rz-button-icon-left rzi'])[17]");
        public ILocator SendTimeCouns => _page.Locator("//i[normalize-space()='schedule']");
        public ILocator SelectTime => _page.Locator("//A[@class='link'][text()='16:00']");

        // --- OFFICES & LOCATIONS ---
        public ILocator Office => _page.Locator("(//SPAN)[734]");
        public ILocator COffice => _page.Locator("//span[normalize-space(text())='[HH Regina] 2400 College Ave - Unit 101 - ReginaLocation Nickname^Anxiety & Depression Cases | 55.00CAD PerHour']");
        public ILocator SubEMHCOffice => _page.Locator("(//SPAN)[721]");
        public ILocator OAATOffice => _page.Locator("(//li[@aria-label='[EMHC - Scheduled DC/TC Only(English Only)] 47A8BD6- wEST 1234 - 108 Mile Ranch']//span)[1]");

        // --- FORMS & ASSESSMENTS ---
        public ILocator Minorconsent => _page.Locator("//span[normalize-space(text())='N/A']");
        public ILocator Majorissue => _page.Locator("(//SPAN[@class='rz-dropdown-label rz-inputtext rz-placeholder'])[1]");
        public ILocator Majorissueselect => _page.Locator("//span[normalize-space(text())='Alcohol']");
        public ILocator Currentrisks1 => _page.Locator("//label[normalize-space(text())='Aggressive Behaviour']");
        public ILocator Levelrisk1 => _page.Locator("(//SPAN[@class='rz-dropdown-trigger-icon  rzi rzi-chevron-down'])[11]");
        public ILocator Levelrisk2 => _page.Locator("(//SPAN[@class='rz-dropdown-trigger-icon  rzi rzi-chevron-down'])[9]");
        public ILocator Levelrisk1select => _page.Locator("//span[text()='Moderate']");
        public ILocator Attendance => _page.Locator("(//div[@class='form-group']//div)[1]");
        public ILocator CAttendance => _page.Locator("(//SPAN[@class='rz-dropdown-trigger-icon  rzi rzi-chevron-down'])[6]");
        public ILocator AttendanceStatus => _page.Locator("//span[text()='Attended']");
        public ILocator DeliveryMethod => _page.Locator("(//SPAN[@class='rz-dropdown-label rz-inputtext rz-placeholder'])[2]");
        public ILocator DeliveryMethodsub => _page.Locator("(//SPAN[@class='rz-dropdown-label rz-inputtext rz-placeholder'])[1]");
        public ILocator DeliveryMethodType => _page.Locator("//span[text()='In Person']");
        public ILocator Consent => _page.Locator("(//div[@class='rz-radiobutton-box'])[2]");
        public ILocator Primaryissue => _page.Locator("(//DIV[@class='rz-chkbox-box'])[3]");
        public ILocator Secondaryissue => _page.Locator("(//DIV[@class='rz-chkbox-box'])[5]");
        public ILocator FuncAssessment => _page.Locator("(//label[normalize-space(text())='Sleep Patterns:']/following::textarea)[1]");
        public ILocator FuncAssessment1 => _page.Locator("(//label[normalize-space(text())='Appetite:']/following::textarea)[1]");
        public ILocator FuncAssessment2 => _page.Locator("(//label[normalize-space(text())='Current Mood:']/following::textarea)[1]");
        public ILocator FuncAssessment3 => _page.Locator("(//label[normalize-space(text())='Memory/Concentration:']/following::textarea)[1]");
        public ILocator FuncAssessment4 => _page.Locator("(//label[normalize-space(text())='Medical Problems/Concerns:']/following::textarea)[1]");
        public ILocator Measuring => _page.Locator("(//SPAN[@class='rz-radiobutton-label'])[5]");
        public ILocator Measuringsub => _page.Locator("(//SPAN[@class='rz-radiobutton-label'])[3]");
        public ILocator Measuringprinciples => _page.Locator("(//SPAN[@class='rz-radiobutton-label'])[7]");
        public ILocator Measuringreview => _page.Locator("(//SPAN[@class='rz-radiobutton-label'])[9]");
        public ILocator Substance => _page.Locator("(//LABEL[@class='rz-chkbox-label'])[10]");
        public ILocator Substanceuse => _page.Locator("(//SPAN[@class='rz-radiobutton-label'])[12]");
        public ILocator SubstanceuseComplete => _page.Locator("(//SPAN[@class='rz-radiobutton-label'])[15]");
        public ILocator SubstanceuseComplete1 => _page.Locator("(//SPAN[@class='rz-radiobutton-label'])[17]");

        // --- TEXT AREAS (Bulk) ---
        public ILocator Textarea1 => _page.Locator("//TEXTAREA[@id='1733']");
        public ILocator Textareasession => _page.Locator("//TEXTAREA[@id='1775']");
        public ILocator CCRStext => _page.Locator("//TEXTAREA[@id='1932']");
        public ILocator Subgoal => _page.Locator("//TEXTAREA[@id='1958']");
        public ILocator Subgoal1 => _page.Locator("//TEXTAREA[@id='1959']");
        public ILocator CCRSsuicide => _page.Locator("(//SPAN[@class='rz-radiobutton-label'])[21]");
        public ILocator Otherrisk => _page.Locator("(//LABEL[@class='rz-chkbox-label'])[18]");
        public ILocator Clientsafety => _page.Locator("(//SPAN[@class='rz-radiobutton-label'])[26]");
        public ILocator Disability => _page.Locator("//TEXTAREA[@id='1734']");
        public ILocator Textarea2 => _page.Locator("//TEXTAREA[@id='1734']");
        public ILocator Textarearelevant => _page.Locator("//TEXTAREA[@id='1948']");
        public ILocator Disabilityform => _page.Locator("(//SPAN[@class='rz-radiobutton-label'])[28]");
        public ILocator Textarea3 => _page.Locator("//TEXTAREA[@id='106']");
        public ILocator Textarea4 => _page.Locator("//TEXTAREA[@id='107']");
        public ILocator Textarea5 => _page.Locator("//TEXTAREA[@id='96']");
        public ILocator Textarea6 => _page.Locator("//TEXTAREA[@id='98']");
        public ILocator Textarea7 => _page.Locator("//TEXTAREA[@id='104']");
        public ILocator Textarea8 => _page.Locator("//TEXTAREA[@id='103']");
        public ILocator Textarea9 => _page.Locator("//TEXTAREA[@id='1736']");

        // --- ASSESSMENTS & SCALES ---
        public ILocator PMHD => _page.Locator("(//DIV[@class='rz-chkbox-box'])[10]");
        public ILocator Touchpoint => _page.Locator("(//DIV[@class='rz-chkbox-box'])[11]");
        public ILocator MBC => _page.Locator("(//LABEL[@class='rz-chkbox-label'])[18]");
        public ILocator CompletedMeasures => _page.Locator("(//LABEL[@class='rz-chkbox-label'])[20]");
        public ILocator Textarea10 => _page.Locator("//TEXTAREA[@id='1942']");
        public ILocator PHQ9 => _page.Locator("(//LABEL[@class='rz-chkbox-label'])[22]");
        public ILocator Textarea11 => _page.Locator("//TEXTAREA[@id='1943']");
        public ILocator GAD => _page.Locator("(//LABEL[@class='rz-chkbox-label'])[25]");
        public ILocator Textarea12 => _page.Locator("//TEXTAREA[@id='1317']");
        public ILocator PCL => _page.Locator("(//LABEL[@class='rz-chkbox-label'])[28]");
        public ILocator SUBS => _page.Locator("(//LABEL[@class='rz-chkbox-label'])[38]");
        public ILocator Currentrisks => _page.Locator("(//LABEL[@class='rz-chkbox-label'])[52]");
        public ILocator CCSRS => _page.Locator("(//SPAN[@class='rz-radiobutton-label'])[19]");
        public ILocator CCSRSOAAT => _page.Locator("//span[normalize-space(text())='No (explain below)']");
        public ILocator Levelofrisk => _page.Locator("(//LABEL[@class='rz-chkbox-label'])[59]");
        public ILocator Textarea13 => _page.Locator("//TEXTAREA[@id='263']");

        // --- GOALS & CASE REVIEW ---
        public ILocator Goal => _page.Locator("(//LABEL[@class='rz-chkbox-label'])[64]");
        public ILocator Goalattainment => _page.Locator("(//SPAN[@class='rz-dropdown-label rz-inputtext rz-placeholder'])[3]");
        public ILocator GoalattainmentOAAT => _page.Locator("(//SPAN[@class='rz-dropdown-trigger-icon  rzi rzi-chevron-down'])[12]");
        public ILocator GoalattainmentSS => _page.Locator("(//SPAN[@class='rz-dropdown-trigger-icon  rzi rzi-chevron-down'])[10]");
        public ILocator Goalattainment1 => _page.Locator("//span[text()='2-Much Improvement']");
        public ILocator Textarea14 => _page.Locator("//TEXTAREA[@id='1946']");
        public ILocator Textarea141 => _page.Locator("//TEXTAREA[@id='1450']");
        public ILocator Textarea142 => _page.Locator("//TEXTAREA[@id='1451']");
        public ILocator Casestatus => _page.Locator("(//SPAN[@class='rz-dropdown-trigger-icon  rzi rzi-chevron-down'])[10]");
        public ILocator CasestatusOAAT => _page.Locator("(//SPAN[@class='rz-dropdown-trigger-icon  rzi rzi-chevron-down'])[13]");
        public ILocator Caseoption => _page.Locator("//span[normalize-space()='Client to call back for session']");
        public ILocator Textarea15 => _page.Locator("//TEXTAREA[@id='1317']");
        public ILocator Textarea16 => _page.Locator("//TEXTAREA[@id='263']");
        public ILocator Otherresources => _page.Locator("(//LABEL[@class='rz-chkbox-label'])[27]");
        public ILocator Otherresources1 => _page.Locator("(//LABEL[@class='rz-chkbox-label'])[20]");
        public ILocator Save => _page.Locator("//SPAN[@class='rz-button-text'][text()='Save Draft and Close']");

        // --- COUNSELLING FORMS ---
        public ILocator Csessionform => _page.Locator("li[aria-label='First Session Assessment - Counselling (Form)'] span");
        public ILocator CAttendanceStatus => _page.Locator("li[aria-label='Attended'] span");
        public ILocator CDeliveryMethodType => _page.Locator("li[aria-label='In Person'] span");
        public ILocator AddCounselling => _page.Locator("//span[normalize-space()='Add Event']");
        public ILocator CRadio1 => _page.Locator("//span[contains(text(),'Yes – client consents')]");
        public ILocator CRadio2 => _page.Locator("(//SPAN[@class='rz-radiobutton-label'])[5]");
        public ILocator CRadio3 => _page.Locator("(//SPAN[@class='rz-radiobutton-label'])[8]");
        public ILocator Cselect => _page.Locator("(//SPAN[@class='rz-dropdown-label rz-inputtext rz-placeholder'])[3]");
        public ILocator Cselect1 => _page.Locator("(//SPAN[@class='rz-dropdown-label rz-inputtext rz-placeholder'])[2]");
        public ILocator Textarea17 => _page.Locator("//TEXTAREA[@id='Clients_Presenting_Issue_and_Events']");
        public ILocator Textarea18 => _page.Locator("//TEXTAREA[@id='Clients_Functioning_and_Stressors']");
        public ILocator Textarea181 => _page.Locator("//TEXTAREA[@id='491']");
        public ILocator SSRadio1 => _page.Locator("(//SPAN[@class='rz-radiobutton-label'])[7]");
        public ILocator Textarea19 => _page.Locator("//TEXTAREA[@id='Clients_Employment_Status']");
        public ILocator CCheckbox => _page.Locator("(//SPAN[@class='rz-radiobutton-label'])[12]");
        public ILocator Textarea20 => _page.Locator("//TEXTAREA[@id='Clients_Psychosocial_and_Family']");
        public ILocator Textarea21 => _page.Locator("//TEXTAREA[@id='Clients_Sleep_Patterns']");
        public ILocator Textarea22 => _page.Locator("//TEXTAREA[@id='Clients_Appetite']");
        public ILocator Textarea23 => _page.Locator("//TEXTAREA[@id='Clients_Current_Mood']");
        public ILocator Textarea24 => _page.Locator("//TEXTAREA[@id='Clients_Memory_Concentration']");
        public ILocator Textarea25 => _page.Locator("//TEXTAREA[@id='Clients_Suicide_Ideation_Attempts']");
        public ILocator Textarea26 => _page.Locator("//TEXTAREA[@id='Clients_Medical_Problems_Concerns']");
        public ILocator CCheckbox1 => _page.Locator("(//SPAN[@class='rz-radiobutton-label'])[13]");
        public ILocator Criskidentified => _page.Locator("(//LABEL[@class='rz-chkbox-label'])[2]");
        public ILocator Cselect2 => _page.Locator("(//SPAN[@class='rz-dropdown-label rz-inputtext rz-placeholder'])[12]");
        public ILocator CRisk => _page.Locator("//span[text()='Moderate']");
        public ILocator Cselect3 => _page.Locator("(//SPAN[@class='rz-radiobutton-label'])[25]");
        public ILocator Textarea27 => _page.Locator("//TEXTAREA[@id='Additional_Notes']");
        public ILocator Textarea28 => _page.Locator("//TEXTAREA[@id='Goal_1']");
        public ILocator Textarea29 => _page.Locator("//TEXTAREA[@id='Goal_Action_Plan_1']");
        public ILocator Cattainment => _page.Locator("//div[@id='-LzlusRpQE']");
        public ILocator Cattainment1 => _page.Locator("(//LI[@role='option'])[526]");
        public ILocator Ccasestatus => _page.Locator("(//SPAN[@class='rz-dropdown-label rz-inputtext rz-placeholder'])[16]");
        public ILocator Ccasestatus1 => _page.Locator("//span[normalize-space()='Client to schedule next session, as needed']");
        public ILocator CCommunity => _page.Locator("(//LABEL[@class='rz-chkbox-label'])[13]");
        public ILocator CHHIResources => _page.Locator("//label[normalize-space(text())='e-courses']");
        public ILocator CSave => _page.Locator("//SPAN[@class='rz-button-text'][text()='Save Draft and Close']");

        // --- DEPRESSION CARE FORMS ---
        public ILocator Dsessionform => _page.Locator("//li[@aria-label='First Session Assessment (& Billing) Form - Depression Care']//span[1]");
        public ILocator DRadiobutton => _page.Locator("(//SPAN[@class='rz-radiobutton-label'])[7]");
        public ILocator DCheckboxanxiety => _page.Locator("(//SPAN[@class='rz-radiobutton-label'])[9]");
        public ILocator DRadiobutton1 => _page.Locator("(//SPAN[@class='rz-radiobutton-label'])[11]");
        public ILocator DCheckbocbehaviour => _page.Locator("(//LABEL[@class='rz-chkbox-label'])[2]");
        public ILocator DCCRS => _page.Locator("(//SPAN[@class='rz-radiobutton-label'])[28]");
        public ILocator Textarea30 => _page.Locator("//TEXTAREA[@id='Additional_Notes']");
        public ILocator Textarea31 => _page.Locator("//TEXTAREA[@id='Goal_1']");
        public ILocator Textarea32 => _page.Locator("//TEXTAREA[@id='Goal_Action_Plan_1']");
        public ILocator DOutcome => _page.Locator("(//LABEL[@class='rz-chkbox-label'])[9]");

        // --- SESSION / DELIVERY TYPES ---
        public ILocator SAddForm => _page.Locator("//div[@class='rz-dropdown-trigger rz-corner-right']//span[1]");
        public ILocator SSessionForm => _page.Locator("//li[contains(@aria-label,'iCBT Counsellor-Assisted Session Note & Billing Form')]//span[1]");
        public ILocator SDeliverytype => _page.Locator("//li[contains(@aria-label,'Chat')]");
        public ILocator SConsent => _page.Locator("//label[contains(text(),'Yes – client consents')]");
        public ILocator SYes => _page.Locator("(//LABEL[@class='rz-chkbox-label'])[4]");
        public ILocator SRisks => _page.Locator("(//LABEL[@class='rz-chkbox-label'])[12]");
        public ILocator SCCRS => _page.Locator("(//LABEL[@class='rz-chkbox-label'])[14]");
        public ILocator SCommunityresources => _page.Locator("(//LABEL[@class='rz-chkbox-label'])[24]");

        // --- BILLING FORMS & ADMIN NOTES ---
        public ILocator SubEMHC => _page.Locator("//li[@aria-label='EMHC Subsequent Session Note & Billing Form v2']//span[1]");
        public ILocator OAAT => _page.Locator("//li[@aria-label='ONE AT A TIME COUNSELLING (& Billing) FORM']//span[1]");
        public ILocator SingleSession => _page.Locator("//li[@aria-label='Single Session COUNSELLING (& Billing) FORM']//span[1]");
        public ILocator AdminNote => _page.Locator("//span[normalize-space()='Administrative Case Note']");
        public ILocator CaseReview => _page.Locator("//span[normalize-space()='Case Review/Request for Extension']");
        public ILocator CaseClose => _page.Locator("//li[@aria-label='Case Close - Counselling']");
        public ILocator TravelExpense => _page.Locator("//li[@aria-label='Travel - Mileage']");
        public ILocator TravelExpense1 => _page.Locator("//TEXTAREA[@id='Expense_Purpose']");
        public ILocator TravelExpense2 => _page.Locator("//INPUT[@id='Num_of_KMs']");

        // --- CASE REVIEW OPTIONS ---
        public ILocator CaseReview1 => _page.Locator("(//SPAN[@class='rz-dropdown-trigger-icon  rzi rzi-chevron-down'])[2]");
        public ILocator CaseClose1 => _page.Locator("(//div[contains(@class,'rz-dropdown valid')])[2]");
        public ILocator CaseReview2 => _page.Locator("(//li[@aria-label='01']//span)[2]");
        public ILocator CaseClose2 => _page.Locator("(//li[@aria-label='Yes']//span)[2]");
        public ILocator CaseReview3 => _page.Locator("(//span[contains(@class,'rz-dropdown-trigger-icon ')])[3]");
        public ILocator CaseClose3 => _page.Locator("(//span[text()='Select...']/following-sibling::div)[3]");
        public ILocator CaseReview4 => _page.Locator("(//li[@aria-label='1']//span)[2]");
        public ILocator CaseClose4 => _page.Locator("(//li[@aria-label='Yes']//span)[2]");
        public ILocator CaseReview5 => _page.Locator("(//SPAN[@class='rz-dropdown-trigger-icon  rzi rzi-chevron-down'])[4]");
        public ILocator CaseClose5 => _page.Locator("(//label[@class='rz-chkbox-label'])[1]");
        public ILocator CaseReview6 => _page.Locator("(//li[@aria-label='01']//span)[2]");
        public ILocator CaseClose6 => _page.Locator("(//div[contains(@class,'rz-dropdown valid')])[3]");
        public ILocator CaseReview7 => _page.Locator("//TEXTAREA[@id='Treatment_Goal_1']");
        public ILocator CaseClose7 => _page.Locator("//span[text()='Low']");
        public ILocator CaseReview8 => _page.Locator("(//SPAN[@class='rz-dropdown-trigger-icon  rzi rzi-chevron-down'])[5]");
        public ILocator CaseClose8 => _page.Locator("(//div[contains(@class,'rz-dropdown valid')])[5]");
        public ILocator CaseReview9 => _page.Locator("(//li[@aria-label='2-Much Improvement']//span)[2]");
        public ILocator CaseClose9 => _page.Locator("//span[text()='2-Much Improvement']");
        public ILocator CaseReview10 => _page.Locator("//TEXTAREA[@id='Treatment_Goal_2']");
        public ILocator CaseReview11 => _page.Locator("(//SPAN[@class='rz-dropdown-trigger-icon  rzi rzi-chevron-down'])[6]");
        public ILocator CaseReview12 => _page.Locator("//TEXTAREA[@id='Rationale']");
        public ILocator CaseReview13 => _page.Locator("(//span[@class='rz-radiobutton-label'])[1]");
        public ILocator CaseReview14 => _page.Locator("//TEXTAREA[@id='Refer_To']");

        // --- SUBSEQUENT SESSIONS & MISC ---
        public ILocator Textarea333 => _page.Locator("//TEXTAREA[@id='1377']");
        public ILocator SSEmployeesituation => _page.Locator("(//span[@class='rz-radiobutton-label'])[3]");
        public ILocator SubEMHCCheckbox => _page.Locator("(//LABEL[@class='rz-chkbox-label'])[10]");
        public ILocator Textarea33 => _page.Locator("//TEXTAREA[@id='1775']");
        public ILocator Text40 => _page.Locator("//TEXTAREA[@id='1415']");
        public ILocator SUBEMHCPHQ => _page.Locator("(//LABEL[@class='rz-chkbox-label'])[12]");
        public ILocator SUBEMHCGAD => _page.Locator("(//LABEL[@class='rz-chkbox-label'])[15]");
        public ILocator SUBEMHCPCL => _page.Locator("(//LABEL[@class='rz-chkbox-label'])[18]");
        public ILocator Textarea34 => _page.Locator("//TEXTAREA[@id='1754']");
        public ILocator SubEMHCCheckbox1 => _page.Locator("(//LABEL[@class='rz-chkbox-label'])[21]");
        public ILocator Textarea35 => _page.Locator("//TEXTAREA[@id='1784']");
        public ILocator SubEMHCCheckbox2 => _page.Locator("(//LABEL[@class='rz-chkbox-label'])[23]");
        public ILocator SubEMHCCheckbox3 => _page.Locator("(//LABEL[@class='rz-chkbox-label'])[30]");
        public ILocator Textarea36 => _page.Locator("//TEXTAREA[@id='1774']");
        public ILocator SubEMHCCheckbox4 => _page.Locator("(//LABEL[@class='rz-chkbox-label'])[34]");
        public ILocator SubEMHCCheckbox5 => _page.Locator("(//LABEL[@class='rz-chkbox-label'])[39]");
        public ILocator SubEMHCCheckbox6 => _page.Locator("(//LABEL[@class='rz-chkbox-label'])[41]");
        public ILocator SubCounseling => _page.Locator("//li[@aria-label='Subsequent Session - Counselling (Form)']//span[1]");
        public ILocator SubCounselingAttendance => _page.Locator("//span[normalize-space()='Attended']");
        public ILocator SubCounselingDelivery => _page.Locator("//span[normalize-space()='In Person']");
        public ILocator SubCounselingConsent => _page.Locator("(//SPAN[@class='rz-radiobutton-label'])[3]");
        public ILocator SubCounselingDisability => _page.Locator("(//SPAN[@class='rz-radiobutton-label'])[5]");
        public ILocator Textarea37 => _page.Locator("//TEXTAREA[@id='Current_Session_Note']");
        public ILocator SubCounselingBehaviour => _page.Locator("(//LABEL[@class='rz-chkbox-label'])[2]");
        public ILocator Textarea38 => _page.Locator("//TEXTAREA[@id='Clients_Employment_Status']");
        public ILocator SubCounselingResources => _page.Locator("(//LABEL[@class='rz-chkbox-label'])[9]");

        // --- SUBSEQUENT DEPRESSION & ICBT ---
        public ILocator SubDepression => _page.Locator("//li[@aria-label='Subsequent Session (& Billing) Form - Depression Care']//span[1]");
        public ILocator SubDepressionTouchpoint => _page.Locator("(//SPAN[@class='rz-radiobutton-label'])[5]");
        public ILocator SubDepressionDisability => _page.Locator("(//SPAN[@class='rz-radiobutton-label'])[7]");
        public ILocator Textarea39 => _page.Locator("//TEXTAREA[@id='Current_Session_Note']");
        public ILocator SubDepressionRisks => _page.Locator("(//LABEL[@class='rz-chkbox-label'])[8]");
        public ILocator SubDepressionResources => _page.Locator("(//LABEL[@class='rz-chkbox-label'])[9]");
        public ILocator SubSessionSentio => _page.Locator("//li[@aria-label='iCBT Note & Billing Form']");
        public ILocator SubSessionSentioAttendance => _page.Locator("//span[normalize-space()='Attended']");
        public ILocator Textarea40 => _page.Locator("//TEXTAREA[@id='1650']");
        public ILocator SubSessionSentioDisability => _page.Locator("(//LABEL[@class='rz-chkbox-label'])[4]");
        public ILocator SubSessionSentioRisks => _page.Locator("(//LABEL[@class='rz-chkbox-label'])[12]");
        public ILocator SubSessionSentioSafetyplan => _page.Locator("(//LABEL[@class='rz-chkbox-label'])[14]");
        public ILocator SubSessionSentioResources => _page.Locator("(//LABEL[@class='rz-chkbox-label'])[21]");
        public ILocator SubSessionSentioDelivery => _page.Locator("//li[@aria-label='Email']");
    }
}