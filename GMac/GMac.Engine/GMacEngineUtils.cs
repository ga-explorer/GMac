﻿using System;
using System.IO;
using GMac.Engine.Compiler;
using TextComposerLib.Loggers.Progress;
using TextComposerLib.Settings;

namespace GMac.Engine
{
    public static class GMacEngineUtils
    {
        internal static SettingsComposer SettingsDefaults = new SettingsComposer();

        internal static SettingsComposer Settings = new SettingsComposer(SettingsDefaults);


        #region License Template
        private const string LicenseTemplateText =
@"BY DOWNLOADING, INSTALLING AND/OR USING THE SOFTWARE OR CLICKING ""I ACCEPT"" WHEN PROMPTED IN CONJUNCTION THEREWITH, YOU (""LICENSEE"") ACCEPT ALL OF THE TERMS AND CONDITIONS OF THIS LICENSE. IF YOU ARE ACCEPTING THESE TERMS ON BEHALF OF ANOTHER PERSON OR LEGAL ENTITY, YOU REPRESENT AND WARRANT THAT YOU HAVE FULL LEGAL AUTHORITY TO ACCEPT ON BEHALF OF AND BIND THAT PERSON OR LEGAL ENTITY TO THESE TERMS.

Software License Agreement
{software_name}

1. Definitions
In addition to the other definitions set forth herein, the following defined terms shall apply:
""Agreement"" this License Agreement between Ahmad Hosny Eid as Licensor, and Licensee.
""Computer"" any electronic device that accepts information in digital or similar form and manipulates it for a specific result based on a sequence of instructions;
""Intellectual Property Rights"" all intellectual and industrial property rights including patents, know-how, moral rights, registered trademarks, registered designs, utility models, applications for and rights to apply for any of the foregoing, unregistered design rights, unregistered trademarks, rights to prevent passing off for unfair competition and copyright, database rights, topography rights and any other rights in any invention, discovery or process, in each case in the United States and all other countries in the world and together with all renewals and extensions;
""License"" the rights granted to the Licensee in relation to the specific defined version of the Software under this Agreement. The Software is licensed, not sold, and no rights are granted other than those expressly set forth in this License;
""Licensee"" (a) the person who installs the Software on a Computer for his own personal use; or (b) where the Software is installed onto a Computer on behalf of an employer, another person, or entity, the Licensee is the employer, other person or organization on whose behalf the Software has been installed;
""License Fee(s)"" the fees payable by the Licensee to the Licensor, excluding all other relevant taxes where applicable, as detailed by the Licensor from time to time.
""Licensor"" Ahmad Hosny Eid, Port-Said, Egypt;
""Software"" {software_name} and any updates, variations or versions thereof, as developed and owned exclusively by Licensor;

2. Interpretation
2.1. Installing: Any references to ""install"", ""installing"" ""installation"" or ""installed"" in connection with the Software include the downloading of the Software from the Licensor's or any third party's remote server.
2.2. Headings: The headings to the clauses and Schedules of this License are for convenience only and will not affect its construction or interpretation.
2.3. Days/Months: Any reference to a ""day"" or a ""Business Day"" will mean a period of 24 hours running from midnight to midnight. Any reference to ""month"" means a calendar month, except any other consecutive, daily period from a date certain during one month until the same date certain the following month.

3. Preamble
This Agreement governs the relationship between Licensee and Licensor. This Agreement sets the terms, rights, restrictions and obligations on using the Software created and owned by Licensor, as detailed herein.

4. License Grant
Licensor hereby grants Licensee a Personal, Non-assignable & non-transferable, Perpetual, Commercial, Royalty free, Without the rights to create derivative works, Non-exclusive license, all with accordance with the terms set forth and other legal restrictions set forth in 3rd party software used while running Software.
4.1. Limited: Licensee may use Software for the purpose of:
4.1.1. Running Software on Licensee’s Website[s] and Server[s];
4.1.2. Allowing 3rd Parties to run Software on Licensee’s Website[s] and Server[s];
4.1.3. Publishing Software’s output to Licensee and 3rd Parties;
4.1.4. Distribute verbatim copies of Software’s output (including compiled binaries);
4.1.5. Modify Software to suit Licensee’s needs and specifications.
4.2. This license is granted perpetually, as long as you do not materially breach it.
4.3. Binary Restricted: Licensee may sublicense Software as a part of a larger work containing more than Software, distributed solely in Object or Binary form under a personal, non-sublicensable, limited license. Such redistribution shall be limited to unlimited codebases.
4.4. Non Assignable & Non-Transferable: Licensee may not assign or transfer his rights and duties under this license.
4.5. Commercial, Royalty Free: Licensee may use Software for any purpose, including paid-services, without any royalties

5. Term & Termination
The Term of this license shall be until terminated. Licensor may terminate this Agreement, including Licensee’s license in the case where: 
5.1. Licensee became insolvent or otherwise entered into any liquidation process; or
5.2. Licensee exported the Software to any jurisdiction where licensor may not enforce his rights under this agreements in; or
5.3. Licensee was in breach of any of this license's terms and conditions and such breach was not cured, immediately upon notification; or
5.4. Licensee in breach of any of the terms of clause 2 to this license; or
5.5. Licensee otherwise entered into any arrangement which caused Licensor to be unable to enforce his rights under this License.

6. Payment
In consideration of the License granted under clause 2, Licensee shall pay Licensor a fee, via Credit-Card, PayPal or any other mean which Licensor may deem adequate. Failure to perform payment shall construe as material breach of this Agreement.

7. Upgrades, Updates and Fixes
Licensor may provide Licensee, from time to time, with Upgrades, Updates or Fixes, as detailed herein and according to his sole discretion. Licensee hereby warrants to keep the Software up-to-date and install all relevant updates and fixes, and may, at his sole discretion, purchase upgrades, according to the rates set by Licensor. Licensor shall provide any update or Fix free of charge; however, nothing in this Agreement shall require Licensor to provide Updates or Fixes.
7.1 Upgrades: for the purpose of this license, an Upgrade shall be a material amendment in the Software, which contains new features and or major performance improvements and shall be marked as a new version number. For example, should Licensee purchase the Software under version 1.X.X, an upgrade shall commence under number 2.0.0.
7.2. Updates: for the purpose of this license, an update shall be a minor amendment in the Software, which may contain new features or minor improvements and shall be marked as a new sub-version number. For example, should Licensee purchase the Software under version 1.1.X, an upgrade shall commence under number 1.2.0.
7.3. Fix: for the purpose of this license, a fix shall be a minor amendment in the Software, intended to remove bugs or alter minor features which impair the Software's functionality. A fix shall be marked as a new sub-sub-version number. For example, should Licensee purchase Software under version 1.1.1, an upgrade shall commence under number 1.1.2.

8. Support: 
Software is provided under an AS-IS basis and without any support, updates or maintenance. Nothing in this Agreement shall require Licensor to provide Licensee with support or fixes to any bug, failure, mis-performance or other defect in the Software.
8.1. Bug Notification: Licensee may provide Licensor of details regarding any bug, defect or failure in the Software promptly and with no delay from such event; Licensee shall comply with Licensor's request for information regarding bugs, defects or failures and furnish him with information, screenshots and try to reproduce such bugs, defects or failures.
8.2. Feature Request: Licensee may request additional features in Software, provided, however, that (i) Licensee shall waive any claim or right in such feature should feature be developed by Licensor; (ii) Licensee shall be prohibited from developing the feature, or disclose such feature request, or feature, to any 3rd party directly competing with Licensor or any 3rd party which may be, following the development of such feature, in direct competition with Licensor; (iii) Licensee warrants that feature does not infringe any 3rd party patent, trademark, trade-secret or any other intellectual property right; and (iv) Licensee developed, envisioned or created the feature solely by himself.

9. Liability:  
To the extent permitted under Law, the Software is provided under an AS-IS basis. Licensor shall never, and without any limit, be liable for any damage, cost, expense or any other payment incurred by Licensee as a result of Software’s actions, failure, bugs and/or any other interaction between the Software  and Licensee’s end-equipment, computers, other software or any 3rd party, end-equipment, computer or services.  Moreover, Licensor shall never be liable for any defect in source code written by Licensee when relying on the Software or using the Software’s source code.

10. Warranty:  
10.1. Intellectual Property: Licensor hereby warrants that the Software does not violate or infringe any 3rd party claims in regards to intellectual property, patents and/or trademarks and that to the best of its knowledge no legal action has been taken against it for any infringement or violation of any 3rd party intellectual property rights.
10.2. No-Warranty: The Software is provided without any warranty; Licensor hereby disclaims any warranty that the Software shall be error free, without defects or code which may cause damage to Licensee’s computers or to Licensee, and that Software shall be functional. Licensee shall be solely liable to any damage, defect or loss incurred as a result of operating software and undertake the risks contained in running the Software on License’s Server[s] and Website[s].
10.3. Prior Inspection: Licensee hereby states that he inspected the Software thoroughly and found it satisfactory and adequate to his needs, that it does not interfere with his regular operation and that it does meet the standards and scope of his computer systems and architecture. Licensee found that the Software interacts with his development, website and server environment and that it does not infringe any of End User License Agreement of any software Licensee may use in performing his services. Licensee hereby waives any claims regarding the Software's incompatibility, performance, results and features, and warrants that he inspected the Software.

11. No Refunds: 
Licensee warrants that he inspected the Software according to clause (10.3.) and that it is adequate to his needs. Accordingly, as the Software is intangible goods, Licensee shall not be, ever, entitled to any refund, rebate, compensation or restitution for any reason whatsoever, even if the Software contains material flaws.

12. Indemnification: 
Licensee hereby warrants to hold Licensor harmless and indemnify Licensor for any lawsuit brought against it in regards to Licensee’s use of the Software in means that violate, breach or otherwise circumvent this license, Licensor's intellectual property rights or Licensor's title in the Software. Licensor shall promptly notify Licensee in case of such legal action and request Licensee’s consent prior to any settlement in relation to such lawsuit or claim.

13. Governing Law, Jurisdiction: 
Licensee hereby agrees not to initiate class-action lawsuits against Licensor in relation to this license and to compensate Licensor for any legal fees, cost or attorney fees should any claim brought by Licensee against Licensor be denied, in part or in full.
";
        #endregion


        /// <summary>
        /// The full name of this GMac application
        /// </summary>
        public static string AppName { get; } = GMacCompilerFeatures.FeatureSetName;

        /// <summary>
        /// The version of GMac
        /// </summary>
        public static string Version { get; private set; }

        /// <summary>
        /// The copyright
        /// </summary>
        public static string Copyright { get; } = @"Copyright (c) 2016-2021 Ahmad Hosny Eid";

        /// <summary>
        /// The full text of the license
        /// </summary>
        public static string License { get; } = GMacCompilerFeatures.LicenseText;


        public static ProgressComposer Progress { get; internal set; }

        public static void ResetProgress()
        {
            Progress?.Reset();
        }

        public static void SetProgress(ProgressComposer progress)
        {
            Progress = progress;
        }


        private static void InitializeSettings()
        {
            //Set default settings
            SettingsDefaults["maxVSpaceDimension"] = @"10";

            //Define settings file for TextComposerLib
            Settings.FilePath =
                Path.Combine(
                    Path.GetDirectoryName(AppContext.BaseDirectory) ?? string.Empty,
                    "GMacSettings.xml");

            //Try reading settings file
            var result = Settings.UpdateFromFile(false);

            if (string.IsNullOrEmpty(result) == false)
                //Error while reading settings file, try saving a default settings file
                Settings.ChainToFile();
        }

        static GMacEngineUtils()
        {
            //Version = "Version " + 
            //    Application
            //    .ProductVersion
            //    .Split(new[] { '.' }, StringSplitOptions.RemoveEmptyEntries)
            //    .Take(3)
            //    .Concatenate(".");

            Progress = new ProgressComposer();

            InitializeSettings();
        }
    }
}