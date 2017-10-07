using System;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Forms;
using TextComposerLib.Settings;
using TextComposerLib.Text;
using TextComposerLib.Text.Mapped;

namespace TextComposerLib
{
    public static class StringUtils
    {
        internal static SettingsComposer SettingsDefaults = new SettingsComposer();

        internal static SettingsComposer Settings = new SettingsComposer(SettingsDefaults);

        private static readonly string[] LineSplitArray = new[] {"\r\n", "\n"};

        private static readonly SHA256Managed HashString = new SHA256Managed();

        private static readonly char[] HexDigitLower = "0123456789abcdef".ToCharArray();
        
        private static readonly char[] LiteralEncodeEscapeChars;

        //private static readonly string[] QuranExcerptsArray =
        //{
        //    @"بِسْمِ اللَّهِ الرَّحْمَنِ الرَّحِيمِ (1) الْحَمْدُ لِلَّهِ رَبِّ الْعَالَمِينَ (2) الرَّحْمَنِ الرَّحِيمِ (3) مَالِكِ يَوْمِ الدِّينِ (4) إِيَّاكَ نَعْبُدُ وَإِيَّاكَ نَسْتَعِينُ (5) اهْدِنَا الصِّرَاطَ الْمُسْتَقِيمَ (6) صِرَاطَ الَّذِينَ أَنْعَمْتَ عَلَيْهِمْ غَيْرِ الْمَغْضُوبِ عَلَيْهِمْ وَلَا الضَّالِّينَ (7)",
        //    @"الم (1) ذَلِكَ الْكِتَابُ لَا رَيْبَ فِيهِ هُدًى لِلْمُتَّقِينَ (2) الَّذِينَ يُؤْمِنُونَ بِالْغَيْبِ وَيُقِيمُونَ الصَّلَاةَ وَمِمَّا رَزَقْنَاهُمْ يُنْفِقُونَ (3) وَالَّذِينَ يُؤْمِنُونَ بِمَا أُنْزِلَ إِلَيْكَ وَمَا أُنْزِلَ مِنْ قَبْلِكَ وَبِالْآخِرَةِ هُمْ يُوقِنُونَ (4) أُولَئِكَ عَلَى هُدًى مِنْ رَبِّهِمْ وَأُولَئِكَ هُمُ الْمُفْلِحُونَ (5)",
        //    @"اللَّهُ لَا إِلَهَ إِلَّا هُوَ الْحَيُّ الْقَيُّومُ لَا تَأْخُذُهُ سِنَةٌ وَلَا نَوْمٌ لَهُ مَا فِي السَّمَاوَاتِ وَمَا فِي الْأَرْضِ مَنْ ذَا الَّذِي يَشْفَعُ عِنْدَهُ إِلَّا بِإِذْنِهِ يَعْلَمُ مَا بَيْنَ أَيْدِيهِمْ وَمَا خَلْفَهُمْ وَلَا يُحِيطُونَ بِشَيْءٍ مِنْ عِلْمِهِ إِلَّا بِمَا شَاءَ وَسِعَ كُرْسِيُّهُ السَّمَاوَاتِ وَالْأَرْضَ وَلَا يَئُودُهُ حِفْظُهُمَا وَهُوَ الْعَلِيُّ الْعَظِيمُ (255)",
        //    @"آمَنَ الرَّسُولُ بِمَا أُنْزِلَ إِلَيْهِ مِنْ رَبِّهِ وَالْمُؤْمِنُونَ كُلٌّ آمَنَ بِاللَّهِ وَمَلَائِكَتِهِ وَكُتُبِهِ وَرُسُلِهِ لَا نُفَرِّقُ بَيْنَ أَحَدٍ مِنْ رُسُلِهِ وَقَالُوا سَمِعْنَا وَأَطَعْنَا غُفْرَانَكَ رَبَّنَا وَإِلَيْكَ الْمَصِيرُ (285) لَا يُكَلِّفُ اللَّهُ نَفْسًا إِلَّا وُسْعَهَا لَهَا مَا كَسَبَتْ وَعَلَيْهَا مَا اكْتَسَبَتْ رَبَّنَا لَا تُؤَاخِذْنَا إِنْ نَسِينَا أَوْ أَخْطَأْنَا رَبَّنَا وَلَا تَحْمِلْ عَلَيْنَا إِصْرًا كَمَا حَمَلْتَهُ عَلَى الَّذِينَ مِنْ قَبْلِنَا رَبَّنَا وَلَا تُحَمِّلْنَا مَا لَا طَاقَةَ لَنَا بِهِ وَاعْفُ عَنَّا وَاغْفِرْ لَنَا وَارْحَمْنَا أَنْتَ مَوْلَانَا فَانْصُرْنَا عَلَى الْقَوْمِ الْكَافِرِينَ (286)",
        //    @"وَلَوْ أَنَّ أَهْلَ الْقُرَى آمَنُوا وَاتَّقَوْا لَفَتَحْنَا عَلَيْهِمْ بَرَكَاتٍ مِنَ السَّمَاءِ وَالْأَرْضِ وَلَكِنْ كَذَّبُوا فَأَخَذْنَاهُمْ بِمَا كَانُوا يَكْسِبُونَ (96) أَفَأَمِنَ أَهْلُ الْقُرَى أَنْ يَأْتِيَهُمْ بَأْسُنَا بَيَاتًا وَهُمْ نَائِمُونَ (97) أَوَأَمِنَ أَهْلُ الْقُرَى أَنْ يَأْتِيَهُمْ بَأْسُنَا ضُحًى وَهُمْ يَلْعَبُونَ (98) أَفَأَمِنُوا مَكْرَ اللَّهِ فَلَا يَأْمَنُ مَكْرَ اللَّهِ إِلَّا الْقَوْمُ الْخَاسِرُونَ (99)",
        //    @"وَيَا قَوْمِ اسْتَغْفِرُوا رَبَّكُمْ ثُمَّ تُوبُوا إِلَيْهِ يُرْسِلِ السَّمَاءَ عَلَيْكُمْ مِدْرَارًا وَيَزِدْكُمْ قُوَّةً إِلَى قُوَّتِكُمْ وَلَا تَتَوَلَّوْا مُجْرِمِينَ (52)",
        //    @"قُلْ إِنَّ رَبِّي يَبْسُطُ الرِّزْقَ لِمَنْ يَشَاءُ وَيَقْدِرُ وَلَكِنَّ أَكْثَرَ النَّاسِ لَا يَعْلَمُونَ (36) وَمَا أَمْوَالُكُمْ وَلَا أَوْلَادُكُمْ بِالَّتِي تُقَرِّبُكُمْ عِنْدَنَا زُلْفَى إِلَّا مَنْ آمَنَ وَعَمِلَ صَالِحًا فَأُولَئِكَ لَهُمْ جَزَاءُ الضِّعْفِ بِمَا عَمِلُوا وَهُمْ فِي الْغُرُفَاتِ آمِنُونَ (37)",
        //    @"قُلْ إِنَّ رَبِّي يَبْسُطُ الرِّزْقَ لِمَنْ يَشَاءُ مِنْ عِبَادِهِ وَيَقْدِرُ لَهُ وَمَا أَنْفَقْتُمْ مِنْ شَيْءٍ فَهُوَ يُخْلِفُهُ وَهُوَ خَيْرُ الرَّازِقِينَ (39)",
        //    @"وَمَا خَلَقْتُ الْجِنَّ وَالْإِنْسَ إِلَّا لِيَعْبُدُونِ (56) مَا أُرِيدُ مِنْهُمْ مِنْ رِزْقٍ وَمَا أُرِيدُ أَنْ يُطْعِمُونِ (57) إِنَّ اللَّهَ هُوَ الرَّزَّاقُ ذُو الْقُوَّةِ الْمَتِينُ (58)",
        //    @"فَإِذَا بَلَغْنَ أَجَلَهُنَّ فَأَمْسِكُوهُنَّ بِمَعْرُوفٍ أَوْ فَارِقُوهُنَّ بِمَعْرُوفٍ وَأَشْهِدُوا ذَوَيْ عَدْلٍ مِنْكُمْ وَأَقِيمُوا الشَّهَادَةَ لِلَّهِ ذَلِكُمْ يُوعَظُ بِهِ مَنْ كَانَ يُؤْمِنُ بِاللَّهِ وَالْيَوْمِ الْآخِرِ وَمَنْ يَتَّقِ اللَّهَ يَجْعَلْ لَهُ مَخْرَجًا (2) وَيَرْزُقْهُ مِنْ حَيْثُ لَا يَحْتَسِبُ وَمَنْ يَتَوَكَّلْ عَلَى اللَّهِ فَهُوَ حَسْبُهُ إِنَّ اللَّهَ بَالِغُ أَمْرِهِ قَدْ جَعَلَ اللَّهُ لِكُلِّ شَيْءٍ قَدْرًا (3) ",
        //    @"فَقُلْتُ اسْتَغْفِرُوا رَبَّكُمْ إِنَّهُ كَانَ غَفَّارًا (10) يُرْسِلِ السَّمَاءَ عَلَيْكُمْ مِدْرَارًا (11) وَيُمْدِدْكُمْ بِأَمْوَالٍ وَبَنِينَ وَيَجْعَلْ لَكُمْ جَنَّاتٍ وَيَجْعَلْ لَكُمْ أَنْهَارًا (12)",
        //    @"فَاذْكُرُونِي أَذْكُرْكُمْ وَاشْكُرُوا لِي وَلَا تَكْفُرُونِ (152) ",
        //    @"يَا أَيُّهَا الَّذِينَ آمَنُوا كُلُوا مِنْ طَيِّبَاتِ مَا رَزَقْنَاكُمْ وَاشْكُرُوا لِلَّهِ إِنْ كُنْتُمْ إِيَّاهُ تَعْبُدُونَ (172)",
        //    @"وَاللَّهُ أَخْرَجَكُمْ مِنْ بُطُونِ أُمَّهَاتِكُمْ لَا تَعْلَمُونَ شَيْئًا وَجَعَلَ لَكُمُ السَّمْعَ وَالْأَبْصَارَ وَالْأَفْئِدَةَ لَعَلَّكُمْ تَشْكُرُونَ (78)",
        //    @"تُسَبِّحُ لَهُ السَّمَاوَاتُ السَّبْعُ وَالْأَرْضُ وَمَنْ فِيهِنَّ وَإِنْ مِنْ شَيْءٍ إِلَّا يُسَبِّحُ بِحَمْدِهِ وَلَكِنْ لَا تَفْقَهُونَ تَسْبِيحَهُمْ إِنَّهُ كَانَ حَلِيمًا غَفُورًا (44) ",
        //    @"فَاصْبِرْ عَلَى مَا يَقُولُونَ وَسَبِّحْ بِحَمْدِ رَبِّكَ قَبْلَ طُلُوعِ الشَّمْسِ وَقَبْلَ غُرُوبِهَا وَمِنْ آنَاءِ اللَّيْلِ فَسَبِّحْ وَأَطْرَافَ النَّهَارِ لَعَلَّكَ تَرْضَى (130)",
        //    @"فَإِذَا اسْتَوَيْتَ أَنْتَ وَمَنْ مَعَكَ عَلَى الْفُلْكِ فَقُلِ الْحَمْدُ لِلَّهِ الَّذِي نَجَّانَا مِنَ الْقَوْمِ الظَّالِمِينَ (28)",
        //    @"وَلَقَدْ آتَيْنَا دَاوُودَ وَسُلَيْمَانَ عِلْمًا وَقَالَا الْحَمْدُ لِلَّهِ الَّذِي فَضَّلَنَا عَلَى كَثِيرٍ مِنْ عِبَادِهِ الْمُؤْمِنِينَ (15)",
        //    @"فَسُبْحَانَ اللَّهِ حِينَ تُمْسُونَ وَحِينَ تُصْبِحُونَ (17) وَلَهُ الْحَمْدُ فِي السَّمَاوَاتِ وَالْأَرْضِ وَعَشِيًّا وَحِينَ تُظْهِرُونَ (18)",
        //    @"إِذَا جَاءَ نَصْرُ اللَّهِ وَالْفَتْحُ (1) وَرَأَيْتَ النَّاسَ يَدْخُلُونَ فِي دِينِ اللَّهِ أَفْوَاجًا (2) فَسَبِّحْ بِحَمْدِ رَبِّكَ وَاسْتَغْفِرْهُ إِنَّهُ كَانَ تَوَّابًا (3)",
        //    @"وَمَا كَانَ قَوْلَهُمْ إِلَّا أَنْ قَالُوا رَبَّنَا اغْفِرْ لَنَا ذُنُوبَنَا وَإِسْرَافَنَا فِي أَمْرِنَا وَثَبِّتْ أَقْدَامَنَا وَانْصُرْنَا عَلَى الْقَوْمِ الْكَافِرِينَ (147) فَآتَاهُمُ اللَّهُ ثَوَابَ الدُّنْيَا وَحُسْنَ ثَوَابِ الْآخِرَةِ وَاللَّهُ يُحِبُّ الْمُحْسِنِينَ (148)",
        //    @"وَقُلْ رَبِّ أَدْخِلْنِي مُدْخَلَ صِدْقٍ وَأَخْرِجْنِي مُخْرَجَ صِدْقٍ وَاجْعَلْ لِي مِنْ لَدُنْكَ سُلْطَانًا نَصِيرًا (80)",
        //    @"إِذْ أَوَى الْفِتْيَةُ إِلَى الْكَهْفِ فَقَالُوا رَبَّنَا آتِنَا مِنْ لَدُنْكَ رَحْمَةً وَهَيِّئْ لَنَا مِنْ أَمْرِنَا رَشَدًا (10)",
        //    @"وَأَيُّوبَ إِذْ نَادَى رَبَّهُ أَنِّي مَسَّنِيَ الضُّرُّ وَأَنْتَ أَرْحَمُ الرَّاحِمِينَ (83) فَاسْتَجَبْنَا لَهُ فَكَشَفْنَا مَا بِهِ مِنْ ضُرٍّ وَآتَيْنَاهُ أَهْلَهُ وَمِثْلَهُمْ مَعَهُمْ رَحْمَةً مِنْ عِنْدِنَا وَذِكْرَى لِلْعَابِدِينَ (84) ",
        //    @"وَذَا النُّونِ إِذْ ذَهَبَ مُغَاضِبًا فَظَنَّ أَنْ لَنْ نَقْدِرَ عَلَيْهِ فَنَادَى فِي الظُّلُمَاتِ أَنْ لَا إِلَهَ إِلَّا أَنْتَ سُبْحَانَكَ إِنِّي كُنْتُ مِنَ الظَّالِمِينَ (87) فَاسْتَجَبْنَا لَهُ وَنَجَّيْنَاهُ مِنَ الْغَمِّ وَكَذَلِكَ نُنْجِي الْمُؤْمِنِينَ (88) وَزَكَرِيَّا إِذْ نَادَى رَبَّهُ رَبِّ لَا تَذَرْنِي فَرْدًا وَأَنْتَ خَيْرُ الْوَارِثِينَ (89) فَاسْتَجَبْنَا لَهُ وَوَهَبْنَا لَهُ يَحْيَى وَأَصْلَحْنَا لَهُ زَوْجَهُ إِنَّهُمْ كَانُوا يُسَارِعُونَ فِي الْخَيْرَاتِ وَيَدْعُونَنَا رَغَبًا وَرَهَبًا وَكَانُوا لَنَا خَاشِعِينَ (90)",
        //    @" وَقُلْ رَبِّ أَعُوذُ بِكَ مِنْ هَمَزَاتِ الشَّيَاطِينِ (97) وَأَعُوذُ بِكَ رَبِّ أَنْ يَحْضُرُونِ (98)",
        //    @"إِنَّهُ كَانَ فَرِيقٌ مِنْ عِبَادِي يَقُولُونَ رَبَّنَا آمَنَّا فَاغْفِرْ لَنَا وَارْحَمْنَا وَأَنْتَ خَيْرُ الرَّاحِمِينَ (109)",
        //    @"وَقُلْ رَبِّ اغْفِرْ وَارْحَمْ وَأَنْتَ خَيْرُ الرَّاحِمِينَ (118) ",
        //    @"الَّذِي خَلَقَنِي فَهُوَ يَهْدِينِ (78) وَالَّذِي هُوَ يُطْعِمُنِي وَيَسْقِينِ (79) وَإِذَا مَرِضْتُ فَهُوَ يَشْفِينِ (80) وَالَّذِي يُمِيتُنِي ثُمَّ يُحْيِينِ (81) وَالَّذِي أَطْمَعُ أَنْ يَغْفِرَ لِي خَطِيئَتِي يَوْمَ الدِّينِ (82) رَبِّ هَبْ لِي حُكْمًا وَأَلْحِقْنِي بِالصَّالِحِينَ (83) وَاجْعَلْ لِي لِسَانَ صِدْقٍ فِي الْآخِرِينَ (84) وَاجْعَلْنِي مِنْ وَرَثَةِ جَنَّةِ النَّعِيمِ (85) وَاغْفِرْ لِأَبِي إِنَّهُ كَانَ مِنَ الضَّالِّينَ (86) وَلَا تُخْزِنِي يَوْمَ يُبْعَثُونَ (87) يَوْمَ لَا يَنْفَعُ مَالٌ وَلَا بَنُونَ (88) إِلَّا مَنْ أَتَى اللَّهَ بِقَلْبٍ سَلِيمٍ (89) ",
        //    @"فَتَبَسَّمَ ضَاحِكًا مِنْ قَوْلِهَا وَقَالَ رَبِّ أَوْزِعْنِي أَنْ أَشْكُرَ نِعْمَتَكَ الَّتِي أَنْعَمْتَ عَلَيَّ وَعَلَى وَالِدَيَّ وَأَنْ أَعْمَلَ صَالِحًا تَرْضَاهُ وَأَدْخِلْنِي بِرَحْمَتِكَ فِي عِبَادِكَ الصَّالِحِينَ (19)",
        //    @"قَالَ رَبِّ إِنِّي ظَلَمْتُ نَفْسِي فَاغْفِرْ لِي فَغَفَرَ لَهُ إِنَّهُ هُوَ الْغَفُورُ الرَّحِيمُ (16) قَالَ رَبِّ بِمَا أَنْعَمْتَ عَلَيَّ فَلَنْ أَكُونَ ظَهِيرًا لِلْمُجْرِمِينَ (17) ",
        //    @"فَسَقَى لَهُمَا ثُمَّ تَوَلَّى إِلَى الظِّلِّ فَقَالَ رَبِّ إِنِّي لِمَا أَنْزَلْتَ إِلَيَّ مِنْ خَيْرٍ فَقِيرٌ (24)",
        //    @"وَقَالَ رَبُّكُمُ ادْعُونِي أَسْتَجِبْ لَكُمْ إِنَّ الَّذِينَ يَسْتَكْبِرُونَ عَنْ عِبَادَتِي سَيَدْخُلُونَ جَهَنَّمَ دَاخِرِينَ (60) ",
        //    @"وَالَّذِينَ جَاءُوا مِنْ بَعْدِهِمْ يَقُولُونَ رَبَّنَا اغْفِرْ لَنَا وَلِإِخْوَانِنَا الَّذِينَ سَبَقُونَا بِالْإِيمَانِ وَلَا تَجْعَلْ فِي قُلُوبِنَا غِلًّا لِلَّذِينَ آمَنُوا رَبَّنَا إِنَّكَ رَءُوفٌ رَحِيمٌ (10)",
        //    @"قُلْ هُوَ اللَّهُ أَحَدٌ (1) اللَّهُ الصَّمَدُ (2) لَمْ يَلِدْ وَلَمْ يُولَدْ (3) وَلَمْ يَكُنْ لَهُ كُفُوًا أَحَدٌ (4)",
        //    @"قُلْ أَعُوذُ بِرَبِّ الْفَلَقِ (1) مِنْ شَرِّ مَا خَلَقَ (2) وَمِنْ شَرِّ غَاسِقٍ إِذَا وَقَبَ (3) وَمِنْ شَرِّ النَّفَّاثَاتِ فِي الْعُقَدِ (4) وَمِنْ شَرِّ حَاسِدٍ إِذَا حَسَدَ (5)",
        //    @"قُلْ أَعُوذُ بِرَبِّ النَّاسِ (1) مَلِكِ النَّاسِ (2) إِلَهِ النَّاسِ (3) مِنْ شَرِّ الْوَسْوَاسِ الْخَنَّاسِ (4) الَّذِي يُوَسْوِسُ فِي صُدُورِ النَّاسِ (5) مِنَ الْجِنَّةِ وَالنَّاسِ (6)"
        //};

        //public static int QuranExcerptsCount => QuranExcerptsArray.Length;

        //public static string QuranExcerpts(int index)
        //{
        //    return QuranExcerptsArray[index % QuranExcerptsArray.Length];
        //}


        static StringUtils()
        {
            // Per http://msdn.microsoft.com/en-us/library/h21280bw.aspx
            var escapes = new[] 
                { "\aa", "\bb", "\ff", "\nn", "\rr", "\tt", "\vv", "\"\"", "\\\\", "??", "\00" };

            LiteralEncodeEscapeChars = new char[escapes.Max(e => e[0]) + 1];
            
            foreach (var escape in escapes)
                LiteralEncodeEscapeChars[escape[0]] = escape[1];

            //Set default settings
            SettingsDefaults["graphvizFolder"] = @"C:\Program Files (x86)\Graphviz2.38\bin";

            //Define settings file for TextComposerLib
            Settings.FilePath = 
                Path.Combine(
                    Path.GetDirectoryName(Application.ExecutablePath) ?? string.Empty, 
                    "TextComposerLibSettings.xml");

            //Try reading settings file
            var result = Settings.UpdateFromFile(false);

            if (string.IsNullOrEmpty(result) == false)
                //Error wilr reading settings file, tru saving a default settings file
                Settings.ChainToFile();
        }


        /// <summary>
        /// Ignores any leading whitespace and return the first non-whitespace separated text in the given input
        /// For example given "  ABC DE " as input returns "ABC"
        /// </summary>
        /// <param name="text"></param>
        /// <param name="remText"></param>
        /// <returns></returns>
        public static string SplitAtFirstWhitespace(this string text, out string remText)
        {
            //The input has no characters, both parts are empty string
            if (String.IsNullOrEmpty(text))
            {
                remText = String.Empty;
                return String.Empty;
            }

            var startCharPos = 0;

            while (startCharPos < text.Length && Char.IsWhiteSpace(text[startCharPos])) 
                startCharPos++;

            //The input is all whitespace, both parts are empty string
            if (startCharPos > text.Length)
            {
                remText = String.Empty;
                return String.Empty;
            }

            var endCharPos = startCharPos;

            while (endCharPos < text.Length && !Char.IsWhiteSpace(text[endCharPos])) 
                endCharPos++;

            remText = 
                endCharPos > text.Length 
                ? String.Empty 
                : text.Substring(endCharPos).Trim();

            return text.Substring(startCharPos, endCharPos - startCharPos);
        }

        /// <summary>
        /// Returns true if this string is a single line; it contains no '\n' characters
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static bool IsSingleLine(this string text)
        {
            return (text.IndexOf('\n') < 0);
        }

        /// <summary>
        /// Returns true if this string is  null, empty, or a single line; it contains no '\n' characters
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static bool IsEmptyOrSingleLine(this string text)
        {
            return String.IsNullOrEmpty(text) || (text.IndexOf('\n') < 0);
        }

        /// <summary>
        /// Returns true if this string is a multi-line string; it contains at least one '\n' character
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static bool IsMultiLine(this string text)
        {
            return (text.IndexOf('\n') >= 0);
        }

        /// <summary>
        /// Generate a String.Format() text from the given text having left and right delimiters. 
        /// For example given
        /// "let v[i] = v[i] + Multivector(#E[id]# = 'v[i]c[id]')".ToStringFormatCode("[", "]")
        /// Generates:
        /// "String.Format("let v{0} = v{0} + Multivector(#E{1}# = 'v{0}c{1}')", i, id)"
        /// </summary>
        /// <param name="text"></param>
        /// <param name="leftDel"></param>
        /// <param name="rightDel"></param>
        /// <param name="verbatimFlag"></param>
        /// <returns></returns>
        public static string ToStringFormatCode(this string text, string leftDel, string rightDel, bool verbatimFlag = false)
        {
            var textBuilder = new MappingComposer();

            textBuilder
                .SetDelimitedText(text, leftDel, rightDel)
                .UniqueMarkedSegments
                .TransformByMarkedIndexUsing(index => "{" + index + "}");

            if (textBuilder.HasMarkedSegments == false)
                return text.ValueToQuotedLiteral(verbatimFlag);

            //Escape all braces in original text to prevent error when String.Format() is called
            textBuilder
                .UniqueUnmarkedSegments
                .Where(s => s.OriginalText.Contains('{') || s.OriginalText.Contains('}'))
                .TransformUsing(
                    s => s.Replace("{", "{{").Replace("}", "}}")
                    );

            return
                textBuilder
                .UniqueMarkedSegments
                .Select(s => s.InitialText)
                .Concatenate(
                    ", ",
                    "String.Format(" + textBuilder.FinalText.ValueToQuotedLiteral(verbatimFlag) + ", ",
                    ")"
                );
        }

        /// <summary>
        /// Convert the string to the equivalent C# string literal 
        /// (enclosing the string in double quotes)
        /// and inserting escape sequences as necessary.
        /// </summary>
        /// <param name="value">The string to be converted to a C# string literal.</param>
        /// <param name="verbatimFlag"></param>
        /// <returns><paramref name="value"/> represented as a C# string literal.</returns>
        public static string ValueToQuotedLiteral(this string value, bool verbatimFlag = false)
        {
            if (String.IsNullOrEmpty(value)) return "\"\"";

            var sb = new StringBuilder(value.Length + 2);

            if (verbatimFlag)
            {
                sb.Append("@\"");

                foreach (var c in value)
                    if (c == '"') 
                        sb.Append("\"\"");
                    //else if (c == '{' && escapeBracesFlag)
                    //    sb.Append("{{");
                    //else if (c == '}' && escapeBracesFlag)
                    //    sb.Append("}}");
                    else 
                        sb.Append(c);

                return sb.Append('"').ToString();
            }

            sb.Append('"');

            foreach (var c in value)
            {
                if (c < LiteralEncodeEscapeChars.Length && '\0' != LiteralEncodeEscapeChars[c])
                    sb.Append('\\').Append(LiteralEncodeEscapeChars[c]);

                else if ('~' >= c && c >= ' ')
                    sb.Append(c);

                else
                    sb.Append(@"\x")
                        .Append(HexDigitLower[c >> 12 & 0x0F])
                        .Append(HexDigitLower[c >> 8 & 0x0F])
                        .Append(HexDigitLower[c >> 4 & 0x0F])
                        .Append(HexDigitLower[c & 0x0F]);
            }

            return sb.Append('"').ToString();
        }

        /// <summary>
        /// Convert the string to the equivalent C# string literal 
        /// (without enclosing the string in double quotes)
        /// and inserting escape sequences as necessary.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="verbatimFlag"></param>
        /// <returns></returns>
        public static string ValueToLiteral(this string value, bool verbatimFlag = false)
        {
            if (String.IsNullOrEmpty(value)) return String.Empty;

            var sb = new StringBuilder(value.Length);

            if (verbatimFlag)
            {
                foreach (var c in value)
                    if (c == '"')
                        sb.Append("\"\"");
                    else
                        sb.Append(c);

                return sb.ToString();
            }

            foreach (var c in value)
            {
                if (c < LiteralEncodeEscapeChars.Length && '\0' != LiteralEncodeEscapeChars[c])
                    sb.Append('\\').Append(LiteralEncodeEscapeChars[c]);

                else if ('~' >= c && c >= ' ')
                    sb.Append(c);

                else
                    sb.Append(@"\x")
                        .Append(HexDigitLower[c >> 12 & 0x0F])
                        .Append(HexDigitLower[c >> 8 & 0x0F])
                        .Append(HexDigitLower[c >> 4 & 0x0F])
                        .Append(HexDigitLower[c & 0x0F]);
            }

            return sb.ToString();
        }

        public static string DoubleQuote(this double value)
        {
            return
                new StringBuilder(32)
                .Append('"')
                .Append(value.ToString(CultureInfo.InvariantCulture))
                .Append('"')
                .ToString();
        }

        public static string DoubleQuote(this float value)
        {
            return
                new StringBuilder(32)
                .Append('"')
                .Append(value.ToString(CultureInfo.InvariantCulture))
                .Append('"')
                .ToString();
        }

        public static string DoubleQuote(this bool value)
        {
            return
                new StringBuilder(32)
                .Append('"')
                .Append(value.ToString())
                .Append('"')
                .ToString();
        }

        public static string DoubleQuote(this int value)
        {
            return
                new StringBuilder(32)
                .Append('"')
                .Append(value.ToString())
                .Append('"')
                .ToString();
        }

        /// <summary>
        /// Enclose the given string by double quotes. 
        /// If the input is null or empty string an empty string is returned 
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static string DoubleQuote(this string text)
        {
            if (String.IsNullOrEmpty(text))
                return String.Empty;

            return 
                new StringBuilder(text.Length + 2)
                .Append('"')
                .Append(text)
                .Append('"')
                .ToString();
        }

        /// <summary>
        /// Converts a full C# literal string, includng optional starting @ and inclosed within single or 
        /// double quotes, into a normal in-memory string value.
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static string QuoteLiteralToValue(this string text)
        {
            var textLength = text.Length;

            if (textLength < 2)
                throw new InvalidOperationException("Invalid quoted C# literal");

            if ((text[0] == '"' && text[textLength - 1] == '"') || (text[0] == '\'' && text[textLength - 1] == '\''))
                return 
                    textLength == 2
                    ? String.Empty
                    : LiteralToValue(text.Substring(1, text.Length - 2));

            if (text[0] == '@' && ((text[1] == '"' && text[textLength - 1] == '"') || (text[1] == '\'' && text[textLength - 1] == '\'')))
                return
                    textLength == 3
                    ? String.Empty
                    : VerbatimLiteralToValue(text.Substring(2, text.Length - 3));

            throw new InvalidOperationException("Invalid quoted C# literal");
        }

        // --------------------------------------------------------------------------------
        /// <summary>
        /// Converts a C# literal string into a normal in-memory string value.
        /// See here: http://dotneteers.net/blogs/divedeeper/archive/2008/08/03/ParsingCSharpStrings.aspx
        /// </summary>
        /// <param name="source">Source C# literal string.</param>
        /// <returns> 
        /// Normal string representation. 
        /// </returns>
        // --------------------------------------------------------------------------------
        public static string LiteralToValue(this string source)
        {
            var sb = new StringBuilder(source.Length);
            var pos = 0;
            while (pos < source.Length)
            {
                var c = source[pos];
                if (c == '\\')
                {
                    // --- Handle escape sequences
                    pos++;
                    if (pos >= source.Length) throw new ArgumentException("Missing escape sequence");
                    switch (source[pos])
                    {
                        // --- Simple character escapes
                        case '\'': c = '\''; break;
                        case '\"': c = '\"'; break;
                        case '\\': c = '\\'; break;
                        case '0': c = '\0'; break;
                        case 'a': c = '\a'; break;
                        case 'b': c = '\b'; break;
                        case 'f': c = '\f'; break;
                        case 'n': c = ' '; break;
                        case 'r': c = ' '; break;
                        case 't': c = '\t'; break;
                        case 'v': c = '\v'; break;
                        case 'x':
                            // --- Hexa escape (1-4 digits)
                            var hexa = new StringBuilder(10);
                            pos++;
                            if (pos >= source.Length)
                                throw new ArgumentException("Missing escape sequence");
                            c = source[pos];
                            if (Char.IsDigit(c) || (c >= 'a' && c <= 'f') || (c >= 'A' && c <= 'F'))
                            {
                                hexa.Append(c);
                                pos++;
                                if (pos < source.Length)
                                {
                                    c = source[pos];
                                    if (Char.IsDigit(c) || (c >= 'a' && c <= 'f') || (c >= 'A' && c <= 'F'))
                                    {
                                        hexa.Append(c);
                                        pos++;
                                        if (pos < source.Length)
                                        {
                                            c = source[pos];
                                            if (Char.IsDigit(c) || (c >= 'a' && c <= 'f') ||
                                              (c >= 'A' && c <= 'F'))
                                            {
                                                hexa.Append(c);
                                                pos++;
                                                if (pos < source.Length)
                                                {
                                                    c = source[pos];
                                                    if (Char.IsDigit(c) || (c >= 'a' && c <= 'f') ||
                                                      (c >= 'A' && c <= 'F'))
                                                    {
                                                        hexa.Append(c);
                                                        pos++;
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                            c = (char)Int32.Parse(hexa.ToString(), NumberStyles.HexNumber);
                            pos--;
                            break;
                        case 'u':
                            // Unicode hexa escape (exactly 4 digits)
                            pos++;
                            if (pos + 3 >= source.Length)
                                throw new ArgumentException("Unrecognized escape sequence");
                            try
                            {
                                uint charValue = UInt32.Parse(source.Substring(pos, 4),
                                  NumberStyles.HexNumber);
                                c = (char)charValue;
                                pos += 3;
                            }
                            catch (SystemException)
                            {
                                throw new ArgumentException("Unrecognized escape sequence");
                            }
                            break;
                        case 'U':
                            // Unicode hexa escape (exactly 8 digits, first four must be 0000)
                            pos++;
                            if (pos + 7 >= source.Length)
                                throw new ArgumentException("Unrecognized escape sequence");
                            try
                            {
                                uint charValue = UInt32.Parse(source.Substring(pos, 8),
                                  NumberStyles.HexNumber);
                                if (charValue > 0xffff)
                                    throw new ArgumentException("Unrecognized escape sequence");
                                c = (char)charValue;
                                pos += 7;
                            }
                            catch (SystemException)
                            {
                                throw new ArgumentException("Unrecognized escape sequence");
                            }
                            break;
                        default:
                            throw new ArgumentException("Unrecognized escape sequence");
                    }
                }
                pos++;
                sb.Append(c);
            }
            return sb.ToString();
        }

        // --------------------------------------------------------------------------------
        /// <summary>
        /// Converts a C# verbatim literal string into a normal in-memory string value.
        /// See here: http://dotneteers.net/blogs/divedeeper/archive/2008/08/03/ParsingCSharpStrings.aspx
        /// </summary>
        /// <param name="source">Source C# literal string.</param>
        /// <returns>
        /// Normal string representation.
        /// </returns>
        // --------------------------------------------------------------------------------
        public static string VerbatimLiteralToValue(this string source)
        {
            StringBuilder sb = new StringBuilder(source.Length);
            int pos = 0;
            while (pos < source.Length)
            {
                char c = source[pos];
                if (c == '\"')
                {
                    // --- Handle escape sequences
                    pos++;
                    if (pos >= source.Length) throw new ArgumentException("Missing escape sequence");
                    if (source[pos] == '\"') c = '\"';
                    else throw new ArgumentException("Unrecognized escape sequence");
                }
                pos++;
                sb.Append(c);
            }
            return sb.ToString();
        }

        // --------------------------------------------------------------------------------
        /// <summary>
        /// Converts a C# literal string into a normal in-memory character value.
        /// See here: http://dotneteers.net/blogs/divedeeper/archive/2008/08/03/ParsingCSharpStrings.aspx
        /// </summary>
        /// <param name="source">Source C# literal string.</param>
        /// <returns>
        /// Normal char representation.
        /// </returns>
        // --------------------------------------------------------------------------------
        public static char LiteralToCharValue(this string source)
        {
            string result = LiteralToValue(source);
            if (result.Length != 1)
                throw new ArgumentException("Invalid char literal");
            return result[0];
        }

        /// <summary>
        /// Repeat this string a number of times
        /// </summary>
        /// <param name="source"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public static string Repeat(this string source, int count)
        {
            if (count < 1) return String.Empty;

            if (count == 1) return source;

            var s = new StringBuilder(count * source.Length);

            while (count > 0)
            {
                s.Append(source);
                count--;
            }

            return s.ToString();
        }

        /// <summary>
        /// Count number of lines in the given string by counting
        /// how many \n characters are in the string
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static int LinesCount(this string text)
        {
            var n = 0;

            //Faster than text.Count(c == '\n'). 
            //See: http://stackoverflow.com/questions/2557002/what-is-the-fastest-way-to-count-newlines-in-a-large-net-string
            foreach (var c in text)
                if (c == '\n') n++;

            return n + 1;
        }

    public static string ToHtmlSafeString(this string text)
        {
            return WebUtility.HtmlEncode(text);
        }

        public static string[] SplitLines(this string text)
        {
            return 
                String.IsNullOrEmpty(text)
                ? new [] { String.Empty }
                : text.Split(LineSplitArray, StringSplitOptions.None);
        }

        /// <summary>
        /// Similar to Substring but does not raise an error if the startIndex or length
        /// arguments are outside the string boundaries
        /// </summary>
        /// <param name="text"></param>
        /// <param name="startIndex"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        public static string TryGetSubstring(this string text, int startIndex, int length)
        {
            if (startIndex >= text.Length || startIndex + length <= 0)
                return string.Empty;

            if (startIndex < 0)
            {
                length += startIndex;
                startIndex = 0;
            }

            if (startIndex + length > text.Length)
                length -= startIndex + length - text.Length;

            return text.Substring(startIndex, length);
        }


        public static string GetHashSha256(this string text)
        {
            return GetHashSha256(text, Encoding.ASCII);
        }

        public static string GetHashSha256(this string text, Encoding textEncoding)
        {
            var bytes = textEncoding.GetBytes(text);

            return GetHashSha256(bytes);
        }

        public static string GetHashSha256(this byte[] bytes)
        {
            var hash = HashString.ComputeHash(bytes);

            var hashString = new StringBuilder(2 * hash.Length);

            foreach (var x in hash)
                hashString.AppendFormat("{0:X2}", x);

            return hashString.ToString();
        }


        public static string FormatAsTable(this string[,] items)
        {
            var rows = 1 + items.GetUpperBound(0);
            var cols = 1 + items.GetUpperBound(1);

            var colWidths = new int[cols];

            for (var c = 0; c < cols; c++)
                colWidths[c] = 
                    Enumerable
                    .Range(0, rows)
                    .Select(r => String.IsNullOrEmpty(items[r, c]) ? 0 : items[r, c].Length)
                    .Max();

            var s = new StringBuilder();

            for (var r = 0; r < rows; r++)
            {
                for (var c = 0; c < cols; c++)
                {
                    var item = items[r, c] ?? "";

                    s.Append(item.PadRight(colWidths[c])).Append(" ");
                }

                s.AppendLine();
            }

            return s.ToString();
        }
    }
}
