using Microsoft.Playwright;

namespace Homeweb_3._0_Tests.Objects
{
    public class HomewebLoginObjects
    {
        private readonly IPage _page;

        public HomewebLoginObjects(IPage page)
        {
            _page = page;
        }

        public ILocator Login => _page.Locator("//a[@aria-label='Sign In']");
        public ILocator LoginFR => _page.Locator("//SPAN[@class='text'][text()='Se connecter']");

        public ILocator UserName => _page.Locator("//div[@class='auth-form__center']//input[@id='emailAddress']");

        public ILocator Next => _page.Locator("//button[@type='submit']");
        public ILocator NextPF => _page.Locator("//button[contains(@class,'btn btn-outline-primary-offwhite')]");
        public ILocator NextFR => _page.Locator("(//BUTTON[@type='submit'])[1]");
        public ILocator Password => _page.Locator("(//input[@id='password'])[1]");

        public ILocator Submit => _page.Locator("//button[@type='submit']");
        public ILocator Profile => _page.Locator("//button[@aria-label='Toggle Account Menu']");
        public ILocator ProfileFR => _page.Locator("//button[@aria-label='Basculer le menu du compte']");

        public ILocator Logout => _page.Locator("//a[@aria-label='Sign out']");
        public ILocator LogoutFR => _page.Locator("//span[normalize-space()='Se déconnecter']");
        public ILocator ForgotPassword => _page.Locator("//a[@class='forgot-password']");
        public ILocator Enteremail => _page.Locator("#Email");
        public ILocator Buttonsubmit => _page.Locator("button[type='submit']");

        // Registration Objects Homeweb
        public ILocator Register => _page.Locator("a[title='Register']");
        public ILocator Orgsearch => _page.Locator("input#orgSearchText");

        public ILocator Searchbutton => _page.Locator("//button[@id='btnOrgSearch']");

        public ILocator Selectitem => _page.Locator(".list-group-item > a");
        public ILocator Companycode => _page.Locator("input#clientEnteredCode");
        public ILocator Nextstep => _page.Locator("//button[@id='clientEnteredCodeNext']");
        public ILocator Firstname => _page.Locator("//input[@id='firstname']");

        public ILocator LastName => _page.Locator("input#lastname");
        public ILocator Email => _page.Locator("input#email");
        public ILocator Password1 => _page.Locator("input#password");
        public ILocator CheckPolicy => _page.Locator("input#chkHhiPolicy");
        public ILocator Marketing => _page.Locator("input#noAccepMarket");
        public ILocator NextButton => _page.Locator("button#next");
        public ILocator Employee => _page.Locator("input#employee");
        public ILocator NextButton1 => _page.Locator("button#next");
        public ILocator JobTitle => _page.Locator("input#jobTitle");
        public ILocator RegComplete => _page.Locator("button#completeReg");

        // Registration objects homeweb FR

        public ILocator RegisterFR => _page.Locator("a[title=\"S'inscrire\"]");
        public ILocator OrgsearchFR => _page.Locator("input#orgSearchText");

        public ILocator SearchbuttonFR => _page.Locator("xpath=id(\"btnOrgSearch\")");

        public ILocator SelectitemFR => _page.Locator(".list-group-item > a");
        public ILocator CompanycodeFR => _page.Locator("#registrationCode");
        public ILocator NextstepFR => _page.Locator("xpath=id(\"clientEnteredCodeNext\")");
        public ILocator FirstnameFR => _page.Locator("#firstname");

        public ILocator LastNameFR => _page.Locator("#lastname");
        public ILocator EmailFR => _page.Locator("#email");
        public ILocator Password1FR => _page.Locator("#password");
        public ILocator CheckPolicyFR => _page.Locator("input#chkHhiPolicy");
        public ILocator MarketingFR => _page.Locator("input#noAccepMarket");
        public ILocator NextButtonFR => _page.Locator("#next");
        public ILocator EmployeeFR => _page.Locator("input#employee");
        public ILocator NextButton1FR => _page.Locator("button#next");
        public ILocator JobTitleFR => _page.Locator("input#jobTitle");
        public ILocator RegCompleteFR => _page.Locator("button#completeReg");

        // Landing Page objects

        public ILocator Signup => _page.Locator("a[title='Register']");

        public ILocator Signup1 => _page.Locator("a[title='Sign in']");

        public ILocator Article1 => _page.Locator("//a[@title='Menopause and Mental Health']");
        public ILocator Article2 => _page.Locator("//a[@title='Self-Compassion: Steps you can take to improve your mental health']");
        public ILocator Article3 => _page.Locator("//a[@title='Neurodiversity in Focus: The Energy Drain of Masking']");
        public ILocator Articleextra => _page.Locator("//a[@title='Leading Through Crises: 7 Key Areas for Supporting Employees']");
        public ILocator LanChange => _page.Locator("//span[normalize-space()='FR']");
        public ILocator Article4 => _page.Locator("//a[contains(@title,'Ménopause et santé mentale')]");
        public ILocator Article5 => _page.Locator("//a[@title='Autocompassion : des gestes concrets pour soutenir votre santé mentale']");
        public ILocator Article6 => _page.Locator("//a[@title='La neurodiversité en point de mire : la perte d’énergie liée à la dissimulation ']");

        public ILocator Homewood => _page.Locator("a[href='/en/about']");
        public ILocator Termsofservice => _page.Locator("a[href='/en/terms-of-service']");
        public ILocator Privacypolicy => _page.Locator("a[href='/en/privacy-policy']");
        public ILocator Accessibility => _page.Locator("a[href='/en/accessibility']");
        public ILocator HomewoodFR => _page.Locator("a[href='/fr/about']");
        public ILocator TermsofserviceFR => _page.Locator("a[href='/fr/terms-of-service']");
        public ILocator PrivacypolicyFR => _page.Locator("a[href='/fr/privacy-policy']");
        public ILocator AccessibilityFR => _page.Locator("a[href='/fr/accessibility']");

        public ILocator SignupFR => _page.Locator("a[title=\"S'inscrire\"]");

        public ILocator Signup1FR => _page.Locator("a[title='Se connecter']");

        // Alumni reg objects
        public ILocator Alumnireg => _page.Locator("//INPUT[@id='registrationCode']");
        public ILocator AlumniregFR => _page.Locator("#registrationCode");
        public ILocator Alumnicity => _page.Locator("//INPUT[@id='city']");
        public ILocator AlumnicityFR => _page.Locator("#city");
        public ILocator Addtreatment => _page.Locator("//A[@id='addTreatment']");
        public ILocator AddtreatmentFR => _page.Locator("#addTreatment");
        public ILocator alumicompletereg => _page.Locator("//BUTTON[@id='completeReg']");
        public ILocator alumicompleteregFR => _page.Locator("#completeReg");

        // PBC reg objects
        public ILocator PBCadditionaldetails => _page.Locator("//span[contains(text(),'0407 Holding Ltd. dba Anchor Inn & Suites[44233] [')]");
        public ILocator PBCNextbutton => _page.Locator("//button[@id='register']");

        // Homeweb dashboard 
        public ILocator Checkin => _page.Locator("//A[@href='/app/en/wellness/pulsecheck']");
        public ILocator CheckinFR => _page.Locator("//a[normalize-space()='Accueil']");
        public ILocator Gettingby => _page.Locator("//input[@id='currentFeeling']");
        public ILocator Watchtutorial => _page.Locator("//a[normalize-space()='Watch tutorial']");
        public ILocator WatchtutorialFR => _page.Locator("//a[normalize-space()='Visionner le tutoriel']");
        public ILocator Continue => _page.Locator("//button[@type='button'][normalize-space()='Continue']");
        public ILocator ContinueFR => _page.Locator("//button[@type='button'][normalize-space()='Continuer']");
        public ILocator Moodselect => _page.Locator("//input[@id='mood-excited']");

        public ILocator Moodselectcontinue => _page.Locator("//button[@type='button'][normalize-space()='Continue']");
        public ILocator BacktoDashboard => _page.Locator("//span[normalize-space()='Dashboard']");
        public ILocator BacktoDashboardFR => _page.Locator("//span[normalize-space()='Tableau de bord']");
        public ILocator Launchpathfinder => _page.Locator("//a[normalize-space()='Launch Pathfinder']");
        public ILocator LaunchpathfinderFR => _page.Locator("//a[contains(text(),'Lancer l’Interface Parcours')]");
        public ILocator Browse => _page.Locator("//A[@href='/app/en/resources']");
        public ILocator BrowseFR => _page.Locator("//a[normalize-space()='Consultation Du Site']");
        public ILocator Recommends => _page.Locator("//a[normalize-space()='Read now']");
        public ILocator RecommendsFR => _page.Locator("//div[@class='item item-dashboard item-pathfinder-recommends-v2 col-12 col-lg-8 mt-5 col-12 col-lg-6']//a[1]");
        public ILocator Pulsecheckrecommends => _page.Locator("xpath=id(\"container-manager\")/DIV[1]/SECTION[1]/DIV[1]/DIV[5]/DIV[1]/DIV[1]/A[1]");
        public ILocator Selfdirected => _page.Locator("//div[@class='tile-resource-card col-12 col-md-6 col-lg-4 polaroid']//span[@class='polaroid-link'][normalize-space()='More information']");
        public ILocator SelfdirectedFR => _page.Locator("//div[@class='tile-resource-card col-12 col-md-6 col-lg-4 polaroid']//span[@class='polaroid-link']");
        public ILocator Additionalresources => _page.Locator("//div[@class='tile-resource-card col-12 col-md-6 col-lg-4 mt-4 mt-md-0 polaroid']//span[@class='polaroid-link'][normalize-space()='More information']");
        public ILocator AdditionalresourcesFR => _page.Locator("//div[@class='tile-resource-card col-12 col-md-6 col-lg-4 mt-4 mt-md-0 polaroid']//span[@class='polaroid-link']");
        public ILocator Additionalresources1 => _page.Locator("//p[normalize-space()='The Invisible Wounds of Mental Health Disorders']");
        public ILocator Article => _page.Locator("//span[normalize-space()='5 Minute Read']");
        public ILocator ArticleFR => _page.Locator("//div[@class='row section-dashboard']//div[3]//a[1]//div[2]//span[1]");
        public ILocator Monthreading => _page.Locator("//a[@class='btn btn-secondary btn-icon-end']");

        public ILocator Resources => _page.Locator("//span[normalize-space()='Resources']");
        public ILocator ResourcesFR => _page.Locator("//span[normalize-space()='Ressources']");
        public ILocator Wellness => _page.Locator("//a[@aria-label='Wellness']");
        public ILocator WellnessFR => _page.Locator("//span[normalize-space()='Bien-être']");
        public ILocator LanchangeEN => _page.Locator("//span[@class='text'][normalize-space()='EN']");
        public ILocator Search => _page.Locator("//span[normalize-space()='Search']");
        public ILocator SearchFR => _page.Locator("//span[normalize-space()='Recherche']");


        // Homewebresources

        public ILocator Tools => _page.Locator("//a[normalize-space()='Guided Support']");
        public ILocator ToolsFR => _page.Locator("//A[@href='/app/fr/resources/category/permalink/outils']");
        public ILocator Childcarelocator => _page.Locator("//div[@class='item item-icon-content col-12 col-sm-6']//a[@class='item-link btn-icon-end'][normalize-space()='Access Resource']");
        public ILocator Childcarelocatorpbc => _page.Locator("//div[@class='item item-icon-content item-no-summary col-12 col-sm-6 col-xl-12 mt-4']//a[@class='item-link btn-icon-end'][normalize-space()='Access Resource']");
        public ILocator ChildcarelocatorFR => _page.Locator("//span[contains(text(),'Localisateur de ressources pour les soins aux enfa')]");
        public ILocator Childcarelocatorstart => _page.Locator("//A[@class='btn btn-primary d-inline-block'][text()='Access Childcare Resource Locator by LifestageCare']");
        public ILocator ChildcarelocatorstartFR => _page.Locator("//a[@class='btn btn-primary d-inline-block']");
        public ILocator Childcarelocatorstop => _page.Locator("//BUTTON[@class='btn btn-primary'][text()='Close']");
        public ILocator Childcarelocatoraccept => _page.Locator("//i[@class='fa-solid fa-chevron-right']");

        public ILocator Backtotools => _page.Locator("//a[normalize-space()='Guided Support']");
        public ILocator HealthandWellness => _page.Locator("//div[@class='item item-icon-content item-no-summary col-12 col-sm-6 col-xl-12 mt-4 mt-xl-0']//a[@class='item-link btn-icon-end'][normalize-space()='Access Resource']");
        public ILocator HealthandWellnesspbc => _page.Locator("//div[@class='item item-icon-content item-no-summary col-12 col-sm-6 mt-4 mt-sm-0']//a[@class='item-link btn-icon-end'][normalize-space()='Access Resource']");

        public ILocator HealthandWellnessFR => _page.Locator("//span[normalize-space()='Le Questionnaire santé']");
        public ILocator HealthandWellnesslibrary => _page.Locator("//a[normalize-space()='Head to the library']");
        public ILocator HealthandWellnesslibraryFR => _page.Locator("//a[@class='btn btn-primary d-inline-block']");
        public ILocator Healthriskassessment => _page.Locator("//div[@class='item item-icon-content item-no-summary col-12 col-sm-6 col-xl-12 mt-4']//a[@class='item-link btn-icon-end'][normalize-space()='Access Resource']");
        public ILocator Healthriskassessmentpbc => _page.Locator("//div[@class='item item-icon-content item-no-summary col-12 col-sm-6 col-xl-12 mt-4 mt-xl-0']//a[@class='item-link btn-icon-end'][normalize-space()='Access Resource']");

        public ILocator Healthriskassessmentaccept => _page.Locator("//A[@class='btn btn-primary d-inline-block'][text()='Access Health Risk Assessment']");
        public ILocator Sentio => _page.Locator("//a[normalize-space()='Access Sentio']");
        public ILocator SentioFR => _page.Locator("//span[normalize-space()='Sentio par Homewood Santé']");
        public ILocator SentioStart => _page.Locator("//A[@class='btn btn-primary d-inline-block'][text()='Access Sentio']");
        public ILocator SentioStartpbc => _page.Locator("//a[@class='btn btn-secondary']");
        public ILocator SentioStartFR => _page.Locator("//a[@class='btn btn-primary d-inline-block']");
        public ILocator SentioAccept => _page.Locator("//BUTTON[@data-bs-dismiss='modal'][text()='Accept']");
        public ILocator Sentiostartbutton => _page.Locator("//A[@href='https://sentioapp.com'][text()='Access Sentio iCBT']");
        public ILocator PBCdepression => _page.Locator("//span[normalize-space()='Depression & Anxiety']");
        public ILocator PBCStart => _page.Locator("//A[@class='btn btn-primary d-inline-block'][text()='Access iVolve']");

        //homeweb resources

        public ILocator Webinars => _page.Locator("//a[normalize-space()='Webinars']");
        public ILocator Covid19 => _page.Locator("//div[@class='content']//p[contains(text(),'In this session, Homewood Health will discuss the ')]");
        public ILocator Youtube => _page.Locator("//button[@title='Play']");
        public ILocator BacktoResources => _page.Locator("//span[normalize-space()='Resources']");
        public ILocator Wellnesssessions => _page.Locator("//a[normalize-space()='Wellness Sessions']");
        public ILocator Beyondstigma => _page.Locator("//p[contains(text(),'​In today’s society, there remains a lack of aware')]");
        public ILocator Buildyourresi => _page.Locator("//a[normalize-space()='Student Life']");
        public ILocator MentalHealth => _page.Locator("//a[normalize-space()='Crisis']");
        public ILocator Childmentalpodcast => _page.Locator("//p[contains(text(),'The death of a close colleague or loved one is one')]");
        public ILocator Podcastplay => _page.Locator("//audio[@type='audio/mpeg']");
        public ILocator Adpatingtochange => _page.Locator("//p[contains(text(),'​Most people aren’t fond of change. We like our ha')]");

        //Sentio
        public ILocator SentioAnxiety => _page.Locator("(//a[@class='btn btn-primary'])[2]");
        public ILocator SentioAnxiety1 => _page.Locator("//a[@href='/app/en/program/anxiety/overview']");
        public ILocator SentioMentalHealth => _page.Locator("//a[@href='/app/en/program/mental-health-wellness/overview']");
        public ILocator SentioCoexist => _page.Locator("//a[@href='/app/en/program/coexisting-anxiety-depression/overview']");
        public ILocator SentioMentalHealthbegin => _page.Locator("(//p[text()='Begin your program by completing a brief assessment so we can evaluate if this program is right for you.']/following-sibling::a)[1]");
        public ILocator SentioGetStarted => _page.Locator("(//A[@href='/app/en/sentio/dashboard'][text()='Get started'])[1]");
        public ILocator SentioLearnmore => _page.Locator("//A[@href='/en/about'][text()='Learn More']");
        public ILocator SentioHome => _page.Locator("//IMG[@src='https://homewood-cdn.s3.ca-central-1.amazonaws.com/images/logos/sentio-color-2024.png']");
        public ILocator SentioGetStarted1 => _page.Locator("//A[@href='/en/login'][text()='Get Started']");
        public ILocator SentioCreateAccount => _page.Locator("//A[@href='/en/registration/'][text()='Create Account']");
        public ILocator SentioHowTo => _page.Locator("//A[@href='/en/faq'][text()='How do I create an account?']");
        public ILocator SentioContactHH => _page.Locator("//A[@href='https://homewoodhealth.com/contact/'][text()='Contact Homewood Health']");
        public ILocator SentioAppstore1 => _page.Locator("(//IMG[@class='store-image'])[1]");
        public ILocator SentioAppstore2 => _page.Locator("(//IMG[@class='store-image'])[2]");
        public ILocator SentioAppstore3 => _page.Locator("//IMG[@src='https://homewood-cdn.s3.ca-central-1.amazonaws.com/client/images/uploads/google-play.png']");
        public ILocator SentioAppstore4 => _page.Locator("//IMG[@src='https://homewood-cdn.s3.ca-central-1.amazonaws.com/client/images/uploads/app-store.png']");
        public ILocator SentioAnxietyBegin => _page.Locator("//div[@class='program-start mt-0 pre']//a[@class='btn btn-primary pulse-primary'][normalize-space()='Begin assessment']");
        public ILocator SentioAnxietyQuestion1 => _page.Locator("(//BUTTON[@role='button'][text()='Not at all'])[1]");
        public ILocator SentioAnxietyQuestion2 => _page.Locator("(//BUTTON[@role='button'][text()='Not at all'])[2]");
        public ILocator SentioAnxietyQuestion3 => _page.Locator("(//BUTTON[@role='button'][text()='Not at all'])[3]");
        public ILocator SentioAnxietyQuestion4 => _page.Locator("(//BUTTON[@role='button'][text()='Not at all'])[4]");
        public ILocator SentioAnxietyQuestion5 => _page.Locator("(//BUTTON[@role='button'][text()='Not at all'])[5]");
        public ILocator SentioAnxietyQuestion6 => _page.Locator("(//BUTTON[@role='button'][text()='Not at all'])[6]");
        public ILocator SentioAnxietyQuestion7 => _page.Locator("(//BUTTON[@role='button'][text()='Not at all'])[7]");
        public ILocator SentioAnxietystartprogram => _page.Locator("//button[normalize-space(text())='Start program']");
        public ILocator SentioAnxietycourse => _page.Locator("(//A[@href='/app/en/program/anxiety/selected/64/understanding-anxiety-first-step-toward-relief'])[1]");
        public ILocator SentioAnxietycourse1 => _page.Locator("//div[@class='container-program-progress']//span[@class='view-state'][normalize-space()='Next']");
        public ILocator SentioAnxietycoursesubmit => _page.Locator("//button[@type='submit']");
        public ILocator SentioAnxietycourseelectives => _page.Locator("//button[normalize-space()='Continue with electives']");
        public ILocator SentioAnxietymoodtracker => _page.Locator("//button[@type='submit']");
        public ILocator SentioAnxietymoodcomplete => _page.Locator("//div[@class='container-program-progress']//span[@class='view-state'][normalize-space()='Complete']");
        public ILocator SentioMentalhealthwellness => _page.Locator("//div[@class='container-program-progress']//div[@class='item-title'][normalize-space()='Creating and Maintaining Healthy Boundaries']");
        public ILocator SentioAnxietymoodtracker1 => _page.Locator("//a[@class='btn btn-primary']");
        public ILocator SentioAnxietymoodtracker2 => _page.Locator("//a[normalize-space(text())='Start Task']");
        public ILocator SentioDashboard => _page.Locator("//SPAN[@class='text'][text()='Dashboard']");
        public ILocator SentioWithdraw => _page.Locator("//a[normalize-space()='withdraw']");
        public ILocator SentioWithdraw1 => _page.Locator("//A[@href='/app/en/program/depression/withdraw'][text()='withdraw']");
        public ILocator SentioEndTreatment => _page.Locator("//button[@type='submit']");
        public ILocator SentioDepression => _page.Locator("//a[@href='/app/en/program/depression/overview']");
        public ILocator SentioDepressionQuestion8 => _page.Locator("(//BUTTON[@role='button'][text()='Not at all'])[8]");
        public ILocator SentioDepressionQuestion9 => _page.Locator("(//BUTTON[@role='button'][text()='Not at all'])[9]");
        public ILocator SentioDeperessionBegin => _page.Locator("(//a[@data-pulse-class='pulse-primary'])[1]");
        public ILocator Sentiogeneralassessment => _page.Locator("//a[contains(@class,'btn btn-outline-primary')]");
        public ILocator Sentiogeneralassessmentbegin => _page.Locator("//A[@href='/app/en/sentio/assessments/general/start'][text()='Begin assessment quiz']");
        public ILocator Sentiogeneralassessmentviewprograms => _page.Locator("//A[@class='btn btn-primary'][text()='View programs']");
        public ILocator Aboutsentio => _page.Locator("//a[normalize-space()='About Sentio']");
        public ILocator SentioFAQ => _page.Locator("//a[normalize-space()='FAQs']");
        public ILocator SentioTerms => _page.Locator("//a[normalize-space()='Terms of Service']");
        public ILocator SentioPrivacy => _page.Locator("//a[normalize-space()='Privacy Policy']");
        public ILocator SentioAccessibility => _page.Locator("//a[normalize-space()='Accessibility']");
        public ILocator SentioWelcome => _page.Locator("//a[normalize-space()='Welcome']");

        public ILocator SentioProgramNext => _page.Locator("(//a[contains(@class,'btn btn-primary')])[2]");
        public ILocator SentioTasks => _page.Locator("//SPAN[@class='text'][text()='Tasks']");
        public ILocator SentioTasksjournal => _page.Locator("//SPAN[@class='title'][text()='Thought Journal']");
        public ILocator SentioTasksjournal1 => _page.Locator("//a[@class='btn btn-primary']");
        public ILocator SentioTasksjournal2 => _page.Locator("//textarea[@id='question1']");
        public ILocator SentioTasksjournal3 => _page.Locator("(//SPAN[@class='text'][text()='Next '])[1]");
        public ILocator SentioTasksjournal4 => _page.Locator("//textarea[@id='question2']");
        public ILocator SentioTasksjournal5 => _page.Locator("(//SPAN[@class='text'][text()='Next '])[2]");
        public ILocator SentioTasksjournal6 => _page.Locator("//textarea[@id='question3']");
        public ILocator SentioTasksjournal7 => _page.Locator("(//SPAN[@class='text'][text()='Next '])[3]");
        public ILocator SentioTasksjournal8 => _page.Locator("//textarea[@id='question4']");
        public ILocator SentioTasksjournal9 => _page.Locator("//SPAN[@class='text'][text()='Submit ']");
        public ILocator SentioTasksMood => _page.Locator("//SPAN[@class='title'][text()='Mood Check']");
        public ILocator SentioTasksMood1 => _page.Locator("//label[@for='question1option1']");
        public ILocator SentioTasksCog => _page.Locator("//SPAN[@class='title'][text()='Cognitive Restructuring']");
        public ILocator SentioTasksCog1 => _page.Locator("//button[normalize-space()='Select a previous entry']");
        public ILocator SentioTasksCog2 => _page.Locator("(//div[@class='item-actions']//button)[1]");
        public ILocator SentioTasksCog3 => _page.Locator("(//SPAN[@class='text'][text()='Next '])[4]");
        public ILocator SentioTasksCog4 => _page.Locator("//TEXTAREA[@id='question5']");
        public ILocator SentioAnxietynext5 => _page.Locator("(//SPAN[@class='text'][text()='Next '])[5]");
        public ILocator SentioAnxietynext6 => _page.Locator("(//SPAN[@class='text'][text()='Next '])[6]");
        public ILocator SentioAnxietyquestion6 => _page.Locator("//TEXTAREA[@id='question6']");
        public ILocator SentioAnxietyquestion7 => _page.Locator("//TEXTAREA[@id='question7']");
        public ILocator SentioAnxietyinput1 => _page.Locator("//INPUT[@id='question1option1']");
        public ILocator SentioAnxietyinput2 => _page.Locator("//INPUT[@id='question2option1']");
        public ILocator SentioAnxietyinput3 => _page.Locator("//INPUT[@id='question3option1']");
        public ILocator SentioAnxietyinput4 => _page.Locator("//INPUT[@id='question4option1']");
        public ILocator SentioAnxietyinput5 => _page.Locator("//INPUT[@id='question5option1']");
        public ILocator SentioAnxietyinput6 => _page.Locator("//INPUT[@id='question6option1']");
        public ILocator SentioAnxietyinput7 => _page.Locator("//INPUT[@id='question7option1']");
        public ILocator SentioAnxietynext7 => _page.Locator("(//SPAN[@class='text'][text()='Next '])[7]");
        public ILocator SentioAnxietyinput8 => _page.Locator("//INPUT[@id='question8option1']");
        public ILocator SentioAnxietynext8 => _page.Locator("(//SPAN[@class='text'][text()='Next '])[8]");
        public ILocator SentioAnxietyquestion9 => _page.Locator("//TEXTAREA[@id='question9']");
        public ILocator SentioAnxietyselectentry => _page.Locator("(//BUTTON[@type='button'][text()='Select entry'])[1]");
        public ILocator SentioAnxietyquestion8 => _page.Locator("//TEXTAREA[@id='question8']");
        public ILocator SentioAnxietycompleteprogram => _page.Locator("//BUTTON[@type='submit'][text()='Complete program']");

        //SentioFR
        public ILocator SentioAnxietyFR => _page.Locator("//A[@href='/app/fr/program/anxiete-1/overview'][text()='Voir le programme']");
        public ILocator SentioGetStartedFR => _page.Locator("//A[@href='/app/fr/sentio/dashboard'][text()='Pour commencer']");
        public ILocator SentioAnxietyBeginFR => _page.Locator("(//A[@href='/app/fr/sentio/program/anxiete-1/assessments/start/Q1-17525941257703-F42FD142F40F69'])[1]");
        public ILocator SentioAnxietyQuestion1FR => _page.Locator("(//BUTTON[@role='button'][text()='Pas du tout'])[1]");
        public ILocator SentioAnxietyQuestion2FR => _page.Locator("(//BUTTON[@role='button'][text()='Pas du tout'])[2]");
        public ILocator SentioAnxietyQuestion3FR => _page.Locator("(//BUTTON[@role='button'][text()='Pas du tout'])[3]");
        public ILocator SentioAnxietyQuestion4FR => _page.Locator("(//BUTTON[@role='button'][text()='Pas du tout'])[4]");
        public ILocator SentioAnxietyQuestion5FR => _page.Locator("(//BUTTON[@role='button'][text()='Pas du tout'])[5]");
        public ILocator SentioAnxietyQuestion6FR => _page.Locator("(//BUTTON[@role='button'][text()='Pas du tout'])[6]");
        public ILocator SentioAnxietyQuestion7FR => _page.Locator("(//BUTTON[@role='button'][text()='Pas du tout'])[7]");
        public ILocator SentioAnxietystartprogramFR => _page.Locator("//BUTTON[@id='submitBtn']");
        public ILocator SentioDepressionFR => _page.Locator("//A[@href='/app/fr/program/depression-1/overview'][text()='Voir le programme']");
        public ILocator SentioDepressionQuestion8FR => _page.Locator("(//BUTTON[@role='button'][text()='Pas du tout'])[8]");
        public ILocator SentioDepressionQuestion9FR => _page.Locator("(//BUTTON[@role='button'][text()='Pas du tout'])[9]");
        public ILocator SentioDeperessionBeginFR => _page.Locator("(//A[@href='/app/fr/program/depression-1/assessments/start/Q1-17525941257703-CEBDF601492111'])[1]");
        public ILocator SentiogeneralassessmentFR => _page.Locator("//A[@class='btn btn-outline-primary btn-icon-end']");
        public ILocator SentiogeneralassessmentbeginFR => _page.Locator("(//A[@href='/app/fr/assessments/general/start'])[2]");
        public ILocator SentiogeneralassessmentviewprogramsFR => _page.Locator("//A[@class='btn btn-primary'][text()='Voir les programmes']");
        public ILocator AboutsentioFR => _page.Locator("(//A[@href='/fr/about'][text()='À propos de Sentio'])[2]");
        public ILocator SentioFAQFR => _page.Locator("//A[@href='/fr/faq'][text()='FAQs']");
        public ILocator SentioTermsFR => _page.Locator("(//A[@href='/fr/terms'])[2]");
        public ILocator SentioPrivacyFR => _page.Locator("(//A[@href='/fr/privacy'][text()='Politique de confidentialité'])[2]");
        public ILocator SentioAccessibilityFR => _page.Locator("//A[@href='/fr/accessibility'][text()='Accessibilité']");
        public ILocator SentioProgramNextFR => _page.Locator("(//a[contains(@class,'btn btn-primary')])[2]");

        //Pathfinder

        public ILocator Slider => _page.Locator("input#myFeeling");

        public ILocator Checkin1 => _page.Locator("a.button:nth-child(4)");

        public ILocator Watchtutorial1 => _page.Locator(".dsg-canvas--alignEnd > a");

        public ILocator Continue1 => _page.Locator(".u-hide--palm > .buttonNext");
        public ILocator Takemedashboard => _page.Locator(".buttonOptions:nth-child(2) > span");
        public ILocator Pathfinder => _page.Locator("//a[normalize-space()='Launch Pathfinder']");
        public ILocator PathfinderFR => _page.Locator("//a[contains(text(),'Lancer l’Interface Parcours')]");
        public ILocator Problemissue => _page.Locator("//button[normalize-space()='Mental health & addiction']");
        public ILocator ProblemissueFR => _page.Locator("//button[normalize-space()='Dépendance et santé mentale']");
        public ILocator Problemselect => _page.Locator("button.buttonOptions:nth-child(1)");
        public ILocator backtodashboard => _page.Locator("a.dashboard-link");
        public ILocator Tools1 => _page.Locator("li:nth-child(2) > .button");
        public ILocator Language => _page.Locator("a.siteNav-language");
        public ILocator Article10 => _page.Locator(".resultItem:nth-child(1) .resultTitle");
        public ILocator Video => _page.Locator(".resultItem:nth-child(15) .resultTitle");
        public ILocator sidebar => _page.Locator(".sidebarNav-item:nth-child(6) > a");
        public ILocator podcast => _page.Locator(".resultItem:nth-child(3) .resultTitle");
        public ILocator Forgotpassword => _page.Locator("p:nth-child(1) > a");
        public ILocator Enteremail1 => _page.Locator("input#Email");
        public ILocator Buttonsubmit1 => _page.Locator("button.btn-primary");
        public ILocator PFAssessment => _page.Locator("//button[contains(text(),'Extremely upset to the point that I cannot functio')]");
        public ILocator PFAssessmentFR => _page.Locator("//button[contains(text(),'Bouleversé/e au point de ne pas pouvoir fonctionne')]");
        public ILocator Problemissue1 => _page.Locator("//button[normalize-space()='Grief & bereavement']");
        public ILocator Problemissue1FR => _page.Locator("//button[normalize-space()='Deuil ou pertes affectives']");
        public ILocator PFAssessment1 => _page.Locator("//button[normalize-space()='Never']");
        public ILocator PFAssessment1FR => _page.Locator("//button[normalize-space()='Jamais']");
        public ILocator PFAssessment2 => _page.Locator("//button[normalize-space()='Never']");
        public ILocator PFAssessment3 => _page.Locator("//button[normalize-space()='Never']");
        public ILocator PFAssessment4 => _page.Locator("//button[normalize-space()='Never']");
        public ILocator PFAssessmentbutton => _page.Locator("//a[normalize-space(text())='Get started']");
        public ILocator PFAssessmentbuttonFR => _page.Locator("//a[normalize-space()='Alors commençons']");
        public ILocator Star => _page.Locator("//button[normalize-space(text())='Skip this step']");
        public ILocator Radio => _page.Locator(".radio-group:nth-child(4) > label");
        public ILocator Next1 => _page.Locator("//button[@class='btn btn-outline-primary-offwhite btn-icon-end submit-inner']");
        public ILocator EmailNext => _page.Locator("//button[@type='submit']");
        public ILocator EmailNextPF => _page.Locator("//button[contains(@class,'btn btn-outline-primary-offwhite')]");
        public ILocator Meetnow => _page.Locator("//a[@class='btn btn-outline-muted-white btn-answer']");

        public ILocator SelectDate => _page.Locator("//DIV[@class='dp__cell_inner dp__pointer dp__cell_highlight_active dp__today dp__active_date']");
        public ILocator SelectTime => _page.Locator("div[class='collection collection-provider-matches d-flex'] div:nth-child(1) div:nth-child(1) div:nth-child(2) div:nth-child(1) div:nth-child(2) button:nth-child(1)");
        public ILocator SelectMode => _page.Locator("(//BUTTON[@type='button'])[6]");
        public ILocator SelectYes => _page.Locator("//button[@class='btn btn-tall btn-outline-muted-white btn-booking btn-reschedule']");
        public ILocator SelectText => _page.Locator("//button[normalize-space()='Text']");
        public ILocator SelectTextFR => _page.Locator("//button[normalize-space()='Par message texte']");
        public ILocator SelectNext => _page.Locator("(//BUTTON[@type='submit'])[1]");
        public ILocator SelectCheck => _page.Locator("//INPUT[@id='acceptPhoneTerms']");
        public ILocator SelectDashboard => _page.Locator("//a[@class='btn btn-outline-primary']");
        public ILocator SelectRating => _page.Locator("label:nth-child(3)");
        public ILocator Reschedule => _page.Locator("button.buttonRounded--white:nth-child(2)");
        public ILocator Iunderstandreschedule => _page.Locator("a.buttonOptions:nth-child(1)");
        public ILocator Cancel => _page.Locator("//A[@class='btn btn-outline-primary'][text()=' Cancel ']");
        public ILocator CancelYes => _page.Locator("//BUTTON[@class='btn btn-outline-muted-white btn-booking btn-cancel'][text()=' Yes ']");
        public ILocator EndServices => _page.Locator("//BUTTON[@class='btn-link'][text()=' end services']");
        public ILocator EndServicesFR => _page.Locator("//button[@class='btn-link']");
        public ILocator YesDone => _page.Locator("//a[@class='btn btn-outline-primary']");
        public ILocator YesEnd => _page.Locator("//button[@class='btn btn-outline-muted-white btn-booking btn-cancel cancel-confirm']");
        public ILocator Reason => _page.Locator("//input[@id='640B10D4-B872-4557-9178-B2DC1C222487']");


        //Publisher Sentio

        public ILocator LaunchPub => _page.Locator("//span[normalize-space()='Launch Publisher']");
        public ILocator LaunchPubSentio => _page.Locator("//span[normalize-space()='Access Sentio Content']");
        public ILocator ViewActivities => _page.Locator("//a[normalize-space()='View activities']");
        public ILocator ViewActivitiesFR => _page.Locator("//a[normalize-space()='Voir les activités']");
        public ILocator ViewDashboard => _page.Locator("//span[normalize-space()='Dashboard']");
        public ILocator ViewDashboardFR => _page.Locator("//span[normalize-space()='Tableau de bord']");
        public ILocator ViewCourses => _page.Locator("//a[normalize-space()='View courses']");
        public ILocator ViewCoursesFR => _page.Locator("//a[normalize-space()='Voir les cours']");
        public ILocator ViewPrograms => _page.Locator("//a[normalize-space()='View programs']");
        public ILocator ViewProgramsFR => _page.Locator("//a[normalize-space()='Voir les programmes']");
        public ILocator ViewPages => _page.Locator("//a[normalize-space()='View pages']");
        public ILocator ViewPagesFR => _page.Locator("//a[normalize-space()='Afficher les pages']");
        public ILocator Viewassets => _page.Locator("//a[normalize-space()='View assets']");
        public ILocator ViewassetsFR => _page.Locator("//a[normalize-space()='Afficher les actifs']");
        public ILocator AddActivity => _page.Locator("//button[@aria-label='Add Activity']");
        public ILocator ActivityName => _page.Locator("//INPUT[@id='referenceTitle']");
        public ILocator ActivitySave => _page.Locator("(//BUTTON[@class='btn btn-save text-uppercase btn-icon-spaced'])[1]");
        public ILocator ActivitySearch => _page.Locator("//INPUT[@id='search']");
        public ILocator ActivityPreview => _page.Locator("//button[normalize-space()='Preview']");
        public ILocator ActivityPreviewmobile => _page.Locator("//button[@aria-label='Mobile']");
        public ILocator ActivityPreviewclose => _page.Locator("//BUTTON[@id='modal-close']");
        public ILocator ActivityEdit => _page.Locator("//SPAN[@class='text'][text()='Edit']");
        public ILocator ActivityEditFR => _page.Locator("(//SPAN[@class='text'][text()='Modifier'])[1]");
        public ILocator ActivityEditdetails => _page.Locator("//SPAN[@class='text'][text()='Edit Details']");
        public ILocator ActivityEditdetailsFR => _page.Locator("//SPAN[@class='text'][text()='Modifier les détails']");
        public ILocator ActivityEditsave => _page.Locator("(//BUTTON[@class='btn btn-save text-uppercase btn-icon-spaced'])[2]");
        public ILocator ActivityEditClose => _page.Locator("(//button[@aria-label='Close'])[3]");
        public ILocator ActivityEditDelete => _page.Locator("(//SPAN[@class='text'][text()='Delete'])[1]");
        public ILocator ActivityEditDeleteFR => _page.Locator("(//SPAN[@class='text'][text()='Supprimer'])[1]");
        public ILocator ActivityEditDeleteconfirm => _page.Locator("(//SPAN[@class='text'][text()='Delete'])[2]");
        public ILocator ActivityEditDeleteconfirmFR => _page.Locator("//BUTTON[@class='btn btn-danger btn-icon-spaced mx-1']");
        public ILocator ActivityEditDeleteconfirm1 => _page.Locator("(//SPAN[@class='text'][text()='Delete'])[3]");
        public ILocator ActivityEditDeleteconfirm1FR => _page.Locator("(//SPAN[@class='text'][text()='Supprimer'])[3]");
        public ILocator ViewCoursespub => _page.Locator("//span[normalize-space()='Courses']");
        public ILocator ViewCoursespubFR => _page.Locator("//span[normalize-space()='Cours']");
        public ILocator Coursesadd => _page.Locator("//SPAN[text()='Add']");
        public ILocator CoursesaddFR => _page.Locator("//span[normalize-space()='Ajouter']");
        public ILocator Coursessave => _page.Locator("(//BUTTON[@class='btn btn-save text-uppercase btn-icon-spaced'])[2]");
        public ILocator ViewProgramspub => _page.Locator("//span[normalize-space()='Programs']");
        public ILocator ViewProgramspubFR => _page.Locator("(//SPAN[@class='text'][text()='Programmes'])[1]");
        public ILocator ViewPagespub => _page.Locator("//span[normalize-space()='Pages']");
        public ILocator ViewPagespubFR => _page.Locator("//span[normalize-space()='Pages']");

        public ILocator Pagesave => _page.Locator("(//BUTTON[@class='btn btn-save text-uppercase btn-icon-spaced'])[1]");
        public ILocator Pageedit => _page.Locator("//span[normalize-space()='Edit Status']");
        public ILocator PageeditFR => _page.Locator("(//BUTTON[@type='button'])[2]");
        public ILocator ViewAssets => _page.Locator("//span[normalize-space()='Assets']");
        public ILocator ViewAssetsFR => _page.Locator("//span[normalize-space()='Actifs']");
        public ILocator Assetsupload => _page.Locator("input[name='input-fileUpload']");
        public ILocator AssetName => _page.Locator("//INPUT[@id='title']");
        public ILocator AssetSave => _page.Locator("//BUTTON[@class='btn btn-save text-uppercase btn-icon-spaced']");

        public ILocator AssetSource => _page.Locator("//INPUT[@id='source']");

        // Publisher Homeweb

        public ILocator LaunchPubHomeweb => _page.Locator("//span[normalize-space()='Access Homeweb Content']");
        public ILocator ViewContent => _page.Locator("//a[normalize-space()='View content']");
        public ILocator ViewContentFR => _page.Locator("//a[normalize-space()='Afficher le contenu']");
        public ILocator ViewAuthor => _page.Locator("//a[normalize-space()='View authors']");
        public ILocator ViewAuthorFR => _page.Locator("//a[normalize-space()='Voir les auteurs']");
        public ILocator ViewCategories => _page.Locator("//a[normalize-space()='View categories']");
        public ILocator ViewCategoriesFR => _page.Locator("//a[normalize-space()='Afficher les catégories']");
        public ILocator ViewPagesHomeweb => _page.Locator("//a[normalize-space()='View pages']");
        public ILocator ViewPagesHomewebFR => _page.Locator("//a[normalize-space()='Afficher les pages']");
        public ILocator ViewTags => _page.Locator("//a[normalize-space()='View tags']");
        public ILocator ViewTagsFR => _page.Locator("//a[normalize-space()='Voir les tags']");
        public ILocator AddContent => _page.Locator("//SPAN[text()='Add']");
        public ILocator AddContentFR => _page.Locator("(//BUTTON[@role='button'])[2]");
        public ILocator ContentTitle => _page.Locator("//INPUT[@id='referenceTitle']");
        public ILocator ContentTime => _page.Locator("//INPUT[@id='estimatedTime']");
        public ILocator ContentEdit => _page.Locator("//SPAN[@class='text'][text()='Edit Metadata']");
        public ILocator ContentEditFR => _page.Locator("//span[normalize-space()='Modifier les métadonnées']");
        public ILocator ContentEditSave => _page.Locator("(//BUTTON[@class='btn btn-save text-uppercase btn-icon-spaced'])[1]");
        public ILocator AuthorSelect => _page.Locator("//span[normalize-space()='Authors']");
        public ILocator AuthorSelectFR => _page.Locator("//span[normalize-space()='Auteurs']");
        public ILocator AuthorName => _page.Locator("//INPUT[@id='name']");
        public ILocator CatSelect => _page.Locator("//span[normalize-space()='Categories']");
        public ILocator CatSelectFR => _page.Locator("//span[normalize-space()='Catégories']");
        public ILocator CatName => _page.Locator("//INPUT[@id='name']");
        public ILocator CatEdit => _page.Locator("//span[normalize-space()='Edit Details']");
        public ILocator CatEditFR => _page.Locator("(//BUTTON[@type='button'])[2]");
        public ILocator CatRestrict => _page.Locator("//INPUT[@id='restrictResource']");
        public ILocator TagSelect => _page.Locator("//span[normalize-space()='Tags']");
        public ILocator TagSelectFR => _page.Locator("//span[normalize-space()='Balises']");
        public ILocator TagLabel => _page.Locator("//INPUT[@id='label']");

        //AlumniObjects
        //public ILocator SignupFR => _page.Locator("//a[@title=\"S'inscrire\"]");
        public ILocator SignupFR1 => _page.Locator("//a[@title='Se connecter']");
        public ILocator AlumniFeatured => _page.Locator("//a[@class='btn btn-secondary w-100']");
        public ILocator AlumniFeaturedFR => _page.Locator("//div[@class='tile-resource-card col-12 mb-4 polaroid-list']//p[@class='summary'][contains(text(),'Appelez au 1-855-805-4858 pour parler avec un cons')]");
        public ILocator AlumniFeatured1 => _page.Locator("//div[@class='tile-resource-card col-12 col-md-6 col-lg-4 mb-4 polaroid polaroid-resource-duration']//span[@class='resource-duration'][normalize-space()='2 Minute Read']");
        public ILocator AlumniFeatured1FR => _page.Locator("//div[@class='tile-resource-card col-12 col-sm-6 col-lg-12 mb-4 polaroid polaroid-resource-duration']//span[@class='resource-duration'][normalize-space()='2 minutes de lecture']");
        public ILocator AlumniFeatured2 => _page.Locator("//a[@class='btn btn-secondary btn-icon-end']");
        public ILocator AlumniFeatured2FR => _page.Locator("//span[normalize-space()='2 minutes de video']");
        public ILocator PBCFeature1 => _page.Locator("//div[@class='col-md-6 column-text']//span[@class='resource-duration'][normalize-space()='Self-Directed Service']");
        public ILocator PBCFeature1FR => _page.Locator("//div[@class='col-12 col-lg-8']//div[2]//a[1]//div[2]//span[2]");
        public ILocator PBCFeature2 => _page.Locator("//div[@class='tile-resource-card col-12 col-md-6 col-lg-4 mb-4 polaroid polaroid-resource-duration']//span[@class='resource-duration'][normalize-space()='2 Minute Video']");
        public ILocator PBCFeature2FR => _page.Locator("//div[@class='col-12 col-lg-8']//div[1]//a[1]//div[2]//span[2]");
        public ILocator Searchbox => _page.Locator("//input[@id='searchHomeweb']");
        public ILocator ClickSearch => _page.Locator("//button[@id='search']//i[@role='presentation']");
        public ILocator ArticleSearch => _page.Locator("//SPAN[@class='title h1'][text()='Childcare Resource Locator by LifestageCare']");

        // EQ objects
        public ILocator EQPolicyID => _page.Locator("input#equitablePolicyId");
        public ILocator SearchButton => _page.Locator("button#btnOrgSearch");
        public ILocator OrgSelect => _page.Locator(".list-group-item > a");
        public ILocator SelectPartOrg => _page.Locator("button#register");
        public ILocator EQSentio => _page.Locator("//div[@class='tile-resource-card col-12 col-md-6 col-lg-4 mb-4 polaroid polaroid-resource-duration']//span[@class='resource-duration'][normalize-space()='Self-Directed Service']");
        public ILocator EQSentioFR => _page.Locator("//div[@class='col-12 col-lg-8']//div[2]//a[1]//div[2]//span[2]");
        public ILocator EQMonthArticle => _page.Locator("//a[@class='btn btn-secondary btn-icon-end']");

        //LSOObjects
        public ILocator LSOselect => _page.Locator(".list-group-item:nth-child(1) .form-check-label");
        public ILocator LSOregion => _page.Locator("//SPAN[@data-role='display'][text()='Cochrane, Algoma']");
        public ILocator LSOrole => _page.Locator("(//SPAN[@data-role='display'][text()='Family Member did not know'])[2]");
        public ILocator LSOroleFR => _page.Locator("(//SPAN[@data-role='display'][text()='Autre'])[2]");
        public ILocator Chevron => _page.Locator("//*[@id='orgTree']/UL[1]/LI[1]/DIV[1]/SPAN[3]");

        public ILocator LSORegcomplete => _page.Locator("//BUTTON[@id='register']");

        public ILocator LSO1 => _page.Locator("//a[normalize-space()='Law Society of Ontario']");
        public ILocator LSO2 => _page.Locator("//a[normalize-space()='LawPRO']");
        public ILocator LSO3 => _page.Locator("//a[normalize-space()='The Distress Centres']");
        public ILocator LSO4 => _page.Locator("//a[normalize-space()='PracticePro - Practice Tools']");
        public ILocator LSO5 => _page.Locator("//a[normalize-space()='Canadian Centre on Substance Abuse']");

        public ILocator LSO6 => _page.Locator("//a[normalize-space()='360 degrés de santé mentale']");
        public ILocator LSO7 => _page.Locator("//a[normalize-space()='La santé mentale des femmes']");
        public ILocator LSO8 => _page.Locator("//a[normalize-space()='La santé mentale des hommes']");
        public ILocator LSO9 => _page.Locator("//a[contains(text(),'Les vacances : se relaxer, se revigorer, se retrou')]");

        public ILocator LSODashboard => _page.Locator("//div[@class='col-md-6 column-text']//span[@class='resource-duration'][normalize-space()='2 Minute Read']");
        public ILocator LSODashboardFR => _page.Locator("//div[@class='tile-resource-card col-12 col-md-6 col-lg-4 mb-4 polaroid polaroid-resource-duration']//span[@class='resource-duration'][normalize-space()='2 minutes de lecture']");
        public ILocator LSOHabitsarticle => _page.Locator("//p[normalize-space()='When Habits Become Addictions']");


    }
}