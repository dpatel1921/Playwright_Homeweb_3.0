using Microsoft.Playwright;

namespace WarriorHealthBeta.Objects
{
    internal class WarriorHealthObjects
    {
        private readonly IPage _page;

        public WarriorHealthObjects(IPage page)
        {
            _page = page;
        }

        public ILocator Home => _page.Locator("//a[@class='link home active']");
        public ILocator WHlogo => _page.Locator("//nav[@class='navbar navbar-expand-lg']//img[@alt='Warrior Health']");

        public ILocator Home1 => _page.Locator("//a[contains(@class,'link home')]");
        public ILocator Browse => _page.Locator("//A[@href='/en/browse'][text()='Browse']");
        public ILocator Assessments => _page.Locator("//A[@href='/en/assessments/'][text()='Assessments']");
        public ILocator Search => _page.Locator("//A[@href='/en/resources/search'][text()='Search']");
        public ILocator FAQ => _page.Locator("//A[@href='/en/faq'][text()='FAQs']");
        public ILocator AboutUs => _page.Locator("//A[@href='/en/about'][text()='About Us']");
        public ILocator PSO => _page.Locator("//A[@href='/en/pso/'][text()='For Public Safety Organizations']");
        public ILocator Toggle => _page.Locator("//SPAN[@class='text'][text()='Français']");
        public ILocator ToggleEng => _page.Locator("//SPAN[@class='text'][text()='English']");

        public ILocator ForIndividuals => _page.Locator("//a[@aria-label='For Individuals']");
        public ILocator ForFamilies => _page.Locator("//a[@aria-label='For Families']");
        public ILocator ForOrgs => _page.Locator("//a[normalize-space()='For Organizations']");
        public ILocator PeerSupport => _page.Locator("//a[normalize-space()='24/7 Peer Support']");
        public ILocator CrisisSupport => _page.Locator("//a[normalize-space()='24/7 Crisis Support']");
        public ILocator Poweredby1 => _page.Locator("//div[@class='container-partners']//img[@alt='Wounded Warriors']");
        public ILocator Poweredby2 => _page.Locator("//div[@class='container-partners']//img[@alt='Homewood Health']");
        public ILocator Poweredby3 => _page.Locator("//div[@class='container-partners']//img[@alt='Trillium Health Partners']");
        public ILocator Poweredby4 => _page.Locator("//div[@class='container-partners']//img[@alt='Centre for Addiction and Mental Health']");
        public ILocator Poweredby5 => _page.Locator("//div[@class='container-partners']//img[@alt='Boots on the Ground']");
        public ILocator Poweredby6 => _page.Locator("//div[@class='container-partners']//img[@alt='Government of Ontario']");
        public ILocator ChatBox => _page.Locator("//div[@title='NAVIGATIO​N SUPPORT CHAT']");
        public ILocator ChatMin => _page.Locator("//button[@title='Minimize']//*[name()='svg']");
        public ILocator Coreservice => _page.Locator("//A[@href='/en/browse'][text()='Browse all']");
        public ILocator GetRecommendation => _page.Locator("//A[@href='/en/assessments/'][text()='Get recommendation']");

        public ILocator PSPservice1 => _page.Locator("//P[@class='card-title'][text()='Mental Health Resources']");
        public ILocator PSPservice2 => _page.Locator("//P[@class='card-title'][text()='iCBT']");
        public ILocator PSPservice3 => _page.Locator("//P[@class='card-title'][text()='iCBT Family']");
        public ILocator PSPresource1 => _page.Locator("(//P[@class='card-title'][text()='Crisis Management: Considerations and Support'])[1]");
        public ILocator PSPresource2 => _page.Locator("//P[@class='card-title'][text()='Moral Injury: An Employee’s Guide to Understanding and Coping']");
        public ILocator PSPresource3 => _page.Locator("//P[@class='card-title'][text()='Leadership and Moral Injury: Tips to Support Your Team']");
        public ILocator PSPresource4 => _page.Locator("//P[@class='card-title'][text()='Post-Traumatic Stress Injury (PTSI) in the Workplace: Understanding, Support, and Recovery']");
        public ILocator PSPresource5 => _page.Locator("(//P[@class='card-title'][text()='Supporting First Responders Through Trauma and Stress'])[1]");
        public ILocator PSPresource6 => _page.Locator("//P[@class='card-title'][text()='Supporting Successful Return to Work']");
        public ILocator PSPresource7 => _page.Locator("//P[@class='card-title'][text()='The Invisible Wounds of Mental Health Disorders']");
        public ILocator PSPresource8 => _page.Locator("//P[@class='card-title'][text()='The Many Faces of Post-Traumatic Stress Disorder (PTSD)']");
        public ILocator PSPresource9 => _page.Locator("//P[@class='card-title'][text()='Your Healthy Workplace Strategy: How Employees Can Build a Positive Work Environment']");

        public ILocator PSPFamily1 => _page.Locator("//P[@class='card-title'][text()='Self-Guided ICBT for Spouse or Significant Others of PSP: The SSO Wellbeing Course by PSPNET']");
        public ILocator PSPFamily2 => _page.Locator("//img[@alt='Image for Sentio iCBT: Online Self-Guided Cognitive Behaviour Therapy for Public Safety Personnel & Families']");
        public ILocator PSPFamily3 => _page.Locator("//P[@class='card-title'][text()='Helping Grieving Employees']");
        public ILocator PSPFamily4 => _page.Locator("//p[contains(text(),'Helping Someone You Care About Through Trauma & Sy')]");
        public ILocator PSPFamily5 => _page.Locator("//P[@class='card-title'][text()='How to Tell Your Child About Your Separation or Divorce']");
        public ILocator PSPFamily6 => _page.Locator("//P[@class='card-title'][text()='Retirement and Your Relationship']");
        public ILocator PSPFamily7 => _page.Locator("//P[@class='card-title'][text()='The Importance of Social Connections for Well-Being']");
        public ILocator PSPFamily8 => _page.Locator("//p[normalize-space()='Coping with a Suicide Loss & Navigating Grief']");
        public ILocator PSPFamily9 => _page.Locator("//section[contains(@class,'zone zone-resource-digest resource-grid')]//p[@class='card-title'][normalize-space()='Crisis Management: Considerations and Support']");
        public ILocator PSPFamily10 => _page.Locator("//P[@class='card-title'][text()='Suicide Prevention: What to Look for and How to Help']");
        public ILocator PSPFamily11 => _page.Locator("(//P[@class='card-title'][text()='Supporting First Responders Through Trauma and Stress'])[2]");
        public ILocator PSPFamily12 => _page.Locator("//P[@class='card-title'][text()='Understanding Self-Harm and Suicide: Recognizing the Signs and Finding Support']");

        public ILocator Footer1 => _page.Locator("div[class='col-partners col-12 col-md-8 col-xl-6'] img[alt='Wounded Warriors']");
        public ILocator Footer2 => _page.Locator("div[class='col-partners col-12 col-md-8 col-xl-6'] img[alt='Homewood Health']");
        public ILocator Footer3 => _page.Locator("div[class='col-partners col-12 col-md-8 col-xl-6'] img[alt='Trillium Health Partners']");
        public ILocator Footer4 => _page.Locator("div[class='col-partners col-12 col-md-8 col-xl-6'] img[alt='Centre for Addiction and Mental Health']");
        public ILocator Footer5 => _page.Locator("div[class='col-partners col-12 col-md-8 col-xl-6'] img[alt='Boots on the Ground']");
        public ILocator Footer6 => _page.Locator("div[class='col-partners col-12 col-md-8 col-xl-6'] img[alt='Government of Ontario']");
        public ILocator Footer7 => _page.Locator("a[href='/en/subscribe']");
        public ILocator Footer8 => _page.Locator("a[href='mailto:info@warriorhealth.ca?subject=Inquiry%20from%20Warrior%20Health%20Website']");
        public ILocator Footer9 => _page.Locator("ul[class='footer-nav'] a[target='_blank']");
        public ILocator Footer10 => _page.Locator("footer[class='footer footer-standard footer-en '] li:nth-child(2) a:nth-child(1)");
        public ILocator Footer11 => _page.Locator("a[href='/en/terms-of-service']");
        public ILocator Footer12 => _page.Locator("a[href='/en/privacy-policy']");

        public ILocator Searchitem => _page.Locator("//input[@id='searchResources']");
        public ILocator Browse1 => _page.Locator("//a[normalize-space()='Browse']");
        public ILocator Browse2 => _page.Locator("//A[@href='/en/assessments/'][text()='Get recommendation']");
        public ILocator Browse3 => _page.Locator("//a[normalize-space()='Trauma']");
        public ILocator Browse4 => _page.Locator("//p[contains(text(),'Supporting First Responders Through Trauma and Str')]");
        public ILocator Browse5 => _page.Locator("//a[normalize-space()='PSPNET']");
        public ILocator Browse6 => _page.Locator("//P[@class='card-title'][text()='Therapist-Guided ICBT for PSP: The PSP PTSD Course by PSPNET']");
        public ILocator Browse7 => _page.Locator("//a[normalize-space()='24/7 Peer Support']");
        public ILocator Browse8 => _page.Locator("//a[normalize-space()='24/7 Crisis Support']");
        public ILocator Browse9 => _page.Locator("//a[normalize-space()='Occupationally Aware Healthcare Provider Directory']");
        public ILocator Search1 => _page.Locator("//a[contains(@class,'link search')]");
        public ILocator Search2 => _page.Locator("//span[normalize-space()='Search']");
        public ILocator Search3 => _page.Locator("//P[@class='card-title'][text()='Mindfulness for Anxiety - Body Scan']");
        public ILocator Search4 => _page.Locator("//P[@class='card-title'][text()='Leadership and Moral Injury: Tips to Support Your Team']");
        public ILocator Search5 => _page.Locator("//P[@class='card-title'][text()='How Shift Work Affects Your Sleep Cycles']");

        public ILocator Aboutus1 => _page.Locator("//picture[@class='wh-logo']//img[@alt='Warrior Health']");
        public ILocator Aboutus2 => _page.Locator("//div[@class='partners']//img[@alt='Wounded Warriors']");
        public ILocator Aboutus3 => _page.Locator("//div[@class='partners']//img[@alt='Homewood Health']");
        public ILocator Aboutus4 => _page.Locator("//div[@class='partners']//img[@alt='Trillium Health Partners']");
        public ILocator Aboutus5 => _page.Locator("//div[@class='partners']//img[@alt='Centre for Addiction and Mental Health']");
        public ILocator Aboutus6 => _page.Locator("//div[@class='partners']//img[@alt='Boots on the Ground']");

        public ILocator Enroll1 => _page.Locator("//div[@class='col-12 col-xl-6 col-text']//a[@aria-label='Enroll my organization'][normalize-space()='Enroll my organization']");
        public ILocator Enroll2 => _page.Locator("//div[@class='col-12 col-xl-6 col-text']//a[@aria-label='Log in'][normalize-space()='Log in']");
        public ILocator Enroll3 => _page.Locator("//li[contains(text(),'Check the')]//a[contains(text(),'FAQs')]");
        public ILocator Enroll4 => _page.Locator("//A[@target='_blank'][text()='Log in']");
        public ILocator Enroll5 => _page.Locator("//div[@class='col-12 col-xl-7 col-text']//a[@aria-label='Enroll my organization'][normalize-space()='Enroll my organization']");
        public ILocator Enroll6 => _page.Locator("//div[@class='col-12 col-xl-7 col-text']//a[@aria-label='Log in'][normalize-space()='Log in']");

        public ILocator Assessment1 => _page.Locator("//BUTTON[@class='btn btn-primary'][text()='Get Started']");
        public ILocator NewAssessment => _page.Locator("//BUTTON[@type='button'][text()=' Start Screening Measures ']");
        public ILocator Assessment2 => _page.Locator("(//BUTTON[@role='button'][text()='Not at all'])[1]");
        public ILocator Assessment3 => _page.Locator("(//BUTTON[@role='button'][text()='Not at all'])[2]");
        public ILocator Select => _page.Locator("//INPUT[@id='range-undefined']");
        public ILocator Next => _page.Locator("//BUTTON[@role='button'][text()='Next']");

        public ILocator Assessment4 => _page.Locator("(//BUTTON[@role='button'][text()='No'])[1]");
        public ILocator Assessment5 => _page.Locator("(//BUTTON[@role='button'][text()='No'])[2]");
        public ILocator Assessment6 => _page.Locator("(//BUTTON[@role='button'][text()='No'])[3]");
        public ILocator Assessment7 => _page.Locator("(//BUTTON[@role='button'][text()='No'])[4]");
        public ILocator Assessment8 => _page.Locator("//button[normalize-space()='Very Good']");
        public ILocator Assessment9 => _page.Locator("//P[@class='card-title'][text()='Financial Fitness']");
        public ILocator Assessment10 => _page.Locator("(//BUTTON[@role='button'][text()='Neutral'])[1]");
        public ILocator Assessment11 => _page.Locator("(//BUTTON[@role='button'][text()='Neutral'])[2]");
        public ILocator Assessment12 => _page.Locator("(//BUTTON[@role='button'][text()='Neutral'])[3]");
        public ILocator Assessment13 => _page.Locator("(//BUTTON[@role='button'][text()='Neutral'])[4]");
        public ILocator Assessment14 => _page.Locator("(//BUTTON[@role='button'][text()='Neutral'])[5]");
        public ILocator Assessment15 => _page.Locator("(//BUTTON[@role='button'][text()='Neutral'])[6]");

        public ILocator Assessment16 => _page.Locator("(//BUTTON[@role='button'][text()='Never'])[1]");
        public ILocator Assessment17 => _page.Locator("(//BUTTON[@role='button'][text()='Never'])[2]");
        public ILocator Assessment18 => _page.Locator("(//BUTTON[@role='button'][text()='Never'])[3]");
        public ILocator Assessment19 => _page.Locator("(//BUTTON[@role='button'][text()='Never'])[4]");
        public ILocator Assessment20 => _page.Locator("(//BUTTON[@role='button'][text()='Never'])[5]");
        public ILocator Assessment21 => _page.Locator("(//BUTTON[@role='button'][text()='Never'])[6]");

        public ILocator Assessment22 => _page.Locator("(//BUTTON[@role='button'][text()='Did not apply to me at all'])[1]");
        public ILocator Assessment23 => _page.Locator("(//BUTTON[@role='button'][text()='Did not apply to me at all'])[2]");
        public ILocator Assessment24 => _page.Locator("(//BUTTON[@role='button'][text()='Did not apply to me at all'])[3]");
        public ILocator Assessment25 => _page.Locator("(//BUTTON[@role='button'][text()='Did not apply to me at all'])[4]");
        public ILocator Assessment26 => _page.Locator("(//BUTTON[@role='button'][text()='Did not apply to me at all'])[5]");
        public ILocator Assessment27 => _page.Locator("(//BUTTON[@role='button'][text()='Did not apply to me at all'])[6]");
        public ILocator Assessment28 => _page.Locator("(//BUTTON[@role='button'][text()='Did not apply to me at all'])[7]");

        public ILocator OrgEnroll1 => _page.Locator("//INPUT[@id='organization']");
        public ILocator OrgEnroll2 => _page.Locator("//INPUT[@id='firstName']");
        public ILocator OrgEnroll3 => _page.Locator("//INPUT[@id='title']");
        public ILocator OrgEnroll4 => _page.Locator("//INPUT[@id='emailAddress']");
        public ILocator OrgEnroll5 => _page.Locator("//INPUT[@id='phone']");
        public ILocator OrgEnroll6 => _page.Locator("//INPUT[@id='city']");
        public ILocator OrgEnroll7 => _page.Locator("//BUTTON[@class='btn btn-primary'][text()=' Submit ']");
        public ILocator OrgEnroll8 => _page.Locator("//INPUT[@id='lastName']");
        public ILocator OrgEnroll9 => _page.Locator("//INPUT[@id='addressLine1']");
        public ILocator OrgEnroll10 => _page.Locator("//INPUT[@id='postalCode']");
        public ILocator OrgEnroll11 => _page.Locator("//INPUT[@id='authorizedRepresentative']");
        public ILocator EmailSign1 => _page.Locator("//BUTTON[@class='btn btn-primary'][text()=' Sign up ']");

        public ILocator FAQ1 => _page.Locator("//button[normalize-space()='What is Warrior Health?']");
        public ILocator Founder1 => _page.Locator("//A[@href='https://woundedwarriors.ca/']");
        public ILocator Founder2 => _page.Locator("//a[normalize-space(text())='Visit our website']");
        public ILocator Founder3 => _page.Locator("//A[@href='https://www.thp.ca']");
        public ILocator Founder4 => _page.Locator("//A[@href='https://www.camh.ca/']");
        public ILocator Founder5 => _page.Locator("//A[@href='https://www.bootsontheground.ca/']");
        public ILocator Founder6 => _page.Locator("//A[@href='https://woundedwarriors.ca/']");

        public ILocator OrgEmailupdate => _page.Locator("//INPUT[@id='name']");
    }
}