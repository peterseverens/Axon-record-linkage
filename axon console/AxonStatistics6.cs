using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Globalization;
using System.IO;
using System.Text;
using System.Web;

/// <summary>
/// Summary description for Class1
/// </summary>
public class AxonCalc6
{

    //TO DO
    //AUTORECODE DIRECT MAKEN                DONE
    //LEZEN VAN BINAIRE ARRAYS
    //setvar in 16 maken                     DONE
    //Rs voor setvar1 apart                  DOM
    //namen logisch maken rr rs ns rt etc..  DONE


    //STATUS

    public int testarray = 0;

    public string status = "No Status Yet";
    //STATUS FILE

    public string statusFileName = "";

    //ARRAY CONSTRAINTS

    public static int maxVarC = 20;   //8;  //maxvar > then cashing
    public static int maxCatC = 20000;   //999; //999;  //6104; //12;  //maxcatr > then cashing


    public static int maxVar = 20; //7 voor jan !!!
    public static int maxCat = 20000; //6104; //Int16.MaxValue;
    public static int MeanNr = 6;
    public static int MaxNsC = 5000000; //100005;
    public static int MaxNrC = MeanNr * MaxNsC;
    public static int MaxNs = 1000000;
    public static int MaxNr = MeanNr * MaxNs;

    public static int maxRperRecord = 200;

    //static int MaxGrCat = 100;

    //Dbase file

    public string Delimeter;
    public string DataDir;
    public string BinDir;
    public string ResultDir;

    //?? Global DBS As Database
    //?? Global RST As Recordset


    public int[,] freq0 = new int[maxVar, maxCat];
    public int[,] freq1 = new int[maxVar, maxCat];
    public int[,] freq = new int[maxVar, maxCat];

    //GRAPHS

    public static byte NcatGraph = 50;
    public double[] scoresN = new double[0];
    public double[] scoresV = new double[0];

    public int Nsmall = 0;
    //public byte[] GWnr = new byte[50];
    //public int[,] GData = new int[100, 50];
    //public string[,] GLabels = new string[100, 50];
    //public byte[,] GColor = new byte[100, 50];
    //public byte Gnr;

    //ouput
    public string ReportID;
    public string SubReportID;
    public string t;

    //FILES
    //public string DBfile;
    public string rawfile;
    public string SysFile;
    public string OutputFile;
    public string ResultFile;
    //public string DBfileR;
    public string MetaScriptFile;
    public string ScriptFile;

    //DATALABELS
    public string[] BlokLabels = new string[maxVar];  //kc
    public string[] Var0Labels = new string[maxVar];  //vc0
    public string[] VarLabels = new string[maxVar];   //vc
    public string[] Var1Labels = new string[maxVar];  //vc1

    //MISSINGS

    public string[] missingS0 = new string[maxCat];
    public string[] missingS = new string[maxCat];
    public string[] missingS1 = new string[maxCat];

    public Int16[] missing0 = new Int16[maxCat];
    public Int16[] missing = new Int16[maxCat];
    public Int16[] missing1 = new Int16[maxCat];

    //VARIABLES
    //public int k;
    //public int v0;
    //public int v;
    //public int v1;
    public Int16 Kc;
    public Int16 Vc0;
    public Int16 Vc;
    public Int16 Vc1;
    public Int16[] NcVc0 = new Int16[maxVar]; //vc0
    public Int16[] NcVc = new Int16[maxVar]; //vc
    public Int16[] NcVc1 = new Int16[maxVar];
    Int16 NcVc0H = 0;
    Int16 NcVcH = 0;
    Int16 NcVc1H = 0;


    //DATA

    public Int64 Ns;             //number of blocks
    public Int64 Nr;             //number of dependent records

    public Byte[] Nrr = new Byte[MaxNsC]; //ns
                                          //    public int[] Pr = new int[2]; //MaxNs]; //ns
//    public string[] RespNr0 = new string[MaxNsC]; //ns
//    public string[] RespNr = new string[MaxNrC]; //nt
////    public string[,] SetKey0 = new int[MaxNs, maxVar]; //ns,kr,kc
////    public string[,] SetKey = new int[MaxNr, maxVar]; //ns,kr,kc

//    public Int16[,] SetVar0 = new Int16[MaxNsC, maxVarC]; //ns,vr,vc
//    public Int16[,] SetVar = new Int16[MaxNrC, maxVarC]; //nt,vr,vc
//    public Int16[,] SetVar1 = new Int16[MaxNrC, maxVar]; //nt,vc1


    public string[] RespNr0 = new string[1]; //ns
    public string[] RespNr = new string[1]; //nt

    public Int16[,] SetVar0 = new Int16[1, 1]; //ns,vr,vc
    public Int16[,] SetVar = new Int16[1,1]; //nt,vr,vc
    public Int16[,] SetVar1 = new Int16[1, 1]; //nt,vc1


    //RECODED DATA
    public string[,] code0 = new string[maxVar, maxCat];
    public string[,] code = new string[maxVar, maxCat];
    public string[,] code1 = new string[maxVar, maxCat];


    //PROBABILITES

    public float[,] CHIx = new float[maxVar, maxVar]; //vc0 vc
    public float[,] CHIp = new float[maxVar, maxVar]; //vc0 vc
    public float[,] Ratiox = new float[maxVar, maxVar]; //vc0 vc

    Int64[,] posLi = new Int64[maxVar, maxVar];



    Int32[,,] sPP = new Int32[1, 1, 1];  //15000 * 6+1 * 4+1 
    Int32[,,] sPPn = new Int32[1, 1, 1];
    public float[,,] sPPratio = new float[1, 1, 1]; //4,4,vc0 vc
    public float[,,] sPPChi = new float[1, 1, 1]; //4,4,vc0 vc

    //Int32[,,,] PP = new Int32[maxCatC, maxCatC, maxVarC, maxVarC];
    //Int32[,,,] PPn = new Int32[maxCatC, maxCatC, maxVarC, maxVarC];
    //public float[, , ,] PPratio = new float[maxCatC, maxCatC, maxVarC, maxVarC]; //4,4,vc0 vc
    //public float[, , ,] PPChi = new float[maxCatC, maxCatC, maxVarC, maxVarC]; //4,4,vc0 vc

    public float[,] ePPratio = new float[maxVar, maxVar];



    //LinkRecords

    public byte[,] SelectVar = new byte[maxVar, maxVar]; //vc0 vc
    public byte[] SelectVar1 = new byte[maxVar]; //vc1

    public float[] score = new float[MaxNr];
    public byte[] Hscore = new byte[MaxNr];

    public int Nselected;


    //CASH USE
    public Boolean UseProbCache = false;
    public Boolean UseVarKeyCache = false;
    public Boolean UseCache = false;

    //PARAMETERS
    public int CriteriumHandling;
    public double ThresHoldPercentageCriterium;
    public double ThresHoldCriterium;
    public double DifferenceCriterium;
    public int AutoSelection;
    public int Iterations;
    public int Linkmethod;
    public int IterNumber;
    public int LikelihoodSource;
    public int MissingsInclude;

    public double Threshold = 0;

    //OUTPUT LIKELIHOODS

    public int chi2WithinShow;
    public int Chi2BetweenShow;
    public int SelectedIdentifiersShow;
    public int CategoryFrequenciesShow;

    //OUTPUT LINKAGE

    public int DistributionLikelihoodsShow;
    public int DistributionLinkedRecordsNShow;
    public int LinkedSetsShow;
    public int GraphicShow;

    //CACHE
    //cache files path
    string dirNow = ""; //System.Web.HttpContext.Current.Server.MapPath("~");
    //PROB cache
    string binFile;
    FileStream lcach;
    BinaryWriter lbw;
    BinaryReader lbr;
    //KEY0 cache file
    string KeyCacheFile0 = "";
    FileStream kcach0;
    BinaryWriter kbw0;
    //KEY cache file
    string KeyCacheFile = "";
    FileStream kcach;
    BinaryWriter kbw;
    BinaryReader kbr;
    FileStream kcachI;
    BinaryWriter kbwI;
    BinaryReader kbrI;
    //VAR cache file
    string VarCacheFile;
    FileStream vcach;
    BinaryWriter vbw;
    BinaryReader vbr;
    FileStream vcachI;
    BinaryWriter vbwI;
    BinaryReader vbrI;

    public void cacheEnd()
    {
        if (UseCache)
        {
            lcach.Close();
            lbw.Close();
            lbr.Close();
            kcach0.Close();
            kbw0.Close();
            kcach.Close();
            kbw.Close();
            kbr.Close();
            kcachI.Close();
            kbwI.Close();
            kbrI.Close();
            vcach.Close();
            vbw.Close();
            vbr.Close();
            vcachI.Close();
            vbwI.Close();
            vbrI.Close();
        }
    }

    public string arraysIniData(Int64 Ns, Int64 Nr, int maxVarC)
    {
   

     try
        {
            Nrr = new Byte[Ns];                                             
            RespNr0 = new string[Ns];  
            RespNr = new string[Nr];  
            SetVar0 = new Int16[Ns, maxVarC];  
            SetVar = new Int16[Nr, maxVarC];  
            SetVar1 = new Int16[Nr, maxVarC];  
        }
        catch (IOException e)
        {
            t= "\n Cannot create data arrays." ;
        }
        return t;
}
    public string arraysIni(Int64 catN)
    {
        scoresN = new double[NcatGraph + 1];
        scoresV = new double[NcatGraph + 1];
        string t = "";
        try
        {
            sPP = new Int32[catN, Vc0 + 1, Vc+1];
            sPPn = new Int32[catN, Vc0 + 1, Vc + 1];
            sPPratio = new float[catN, Vc0 + 1, Vc + 1]; //4,4,vc0 vc
            sPPChi = new float[catN, Vc0 + 1, Vc + 1] ;
        }
        catch (IOException e)
        {
            t= "\n Cannot create probabilities arrays." ;

        }
        return t;

    }
    public void cacheIni()
    {

        saveStatus(statusFileName, "Building Arrays and Cashe files");

        //Prob chache file
        binFile = dirNow + "/axon/LikelihoodsCach2.bin";
        try
        {
            File.Delete(binFile);
        }
        catch (IOException e)
        {
            t = "no file to delete?";
        }
        try
        {
            FileStream cachtest = new FileStream(binFile, FileMode.OpenOrCreate);
            BinaryWriter bwtest = new BinaryWriter(cachtest);
            cachtest.Close();
            bwtest.Close();
        }
        catch (IOException e)
        {
            Console.WriteLine(e.Message + "\n Cannot create file.");

        }
        //cach.Close();
        lcach = new FileStream(binFile, FileMode.OpenOrCreate);
        lbw = new BinaryWriter(lcach);
        lbr = new BinaryReader(lcach);

        //KEY0 chache file
        KeyCacheFile0 = dirNow + "/axon/Key0Cach.bin";
        try
        {
            File.Delete(KeyCacheFile0);
        }
        catch (IOException e)
        {
            t = "no file to delete?";
        }
        try
        {

            FileStream cachtest = new FileStream(KeyCacheFile0, FileMode.OpenOrCreate);
            BinaryWriter bwtest = new BinaryWriter(cachtest);
            cachtest.Close();
            bwtest.Close();
        }
        catch (IOException e)
        {
            Console.WriteLine(e.Message + "\n Cannot create file.");

            //return sb;
        }
        kcach0 = new FileStream(KeyCacheFile0, FileMode.OpenOrCreate);
        kbw0 = new BinaryWriter(kcach0);

        //KEY chache
        KeyCacheFile = dirNow + "/axon/KeyCach.bin";
        try
        {
            File.Delete(KeyCacheFile);
        }
        catch (IOException e)
        {
            t = "no file to delete?";
        }
        try
        {

            FileStream cachtest = new FileStream(KeyCacheFile, FileMode.OpenOrCreate);
            BinaryWriter bwtest = new BinaryWriter(cachtest);
            cachtest.Close();
            bwtest.Close();
        }
        catch (IOException e)
        {
            Console.WriteLine(e.Message + "\n Cannot create file.");

            //return sb;
        }
        kcach = new FileStream(KeyCacheFile, FileMode.OpenOrCreate);
        kbw = new BinaryWriter(kcach);
        kbr = new BinaryReader(kcach);

        //KEY chache I
        KeyCacheFile = dirNow + "/axon/KeyCachI.bin";
        try
        {
            File.Delete(KeyCacheFile);
        }
        catch (IOException e)
        {
            t = "no file to delete?";
        }
        try
        {

            FileStream cachtest = new FileStream(KeyCacheFile, FileMode.OpenOrCreate);
            BinaryWriter bwtest = new BinaryWriter(cachtest);
            cachtest.Close();
            bwtest.Close();
        }
        catch (IOException e)
        {
            Console.WriteLine(e.Message + "\n Cannot create file.");

            //return sb;
        }
        kcachI = new FileStream(KeyCacheFile, FileMode.OpenOrCreate);
        kbwI = new BinaryWriter(kcachI);
        kbrI = new BinaryReader(kcachI);

        //Var chache Key
        VarCacheFile = dirNow + "/axon/VarCach.bin";
        try
        {
            File.Delete(VarCacheFile);
        }
        catch (IOException e)
        {
            t = "no file to delete?";
        }
        try
        {

            FileStream cachtest = new FileStream(KeyCacheFile, FileMode.OpenOrCreate);
            BinaryWriter bwtest = new BinaryWriter(cachtest);
            cachtest.Close();
            bwtest.Close();
        }
        catch (IOException e)
        {
            Console.WriteLine(e.Message + "\n Cannot create file.");

            //return sb;
        }
        vcach = new FileStream(VarCacheFile, FileMode.OpenOrCreate);
        vbw = new BinaryWriter(vcach);
        vbr = new BinaryReader(vcach);

        //Var chache Info
        VarCacheFile = dirNow + "/axon/VarCachI.bin";
        try
        {
            File.Delete(VarCacheFile);
        }
        catch (IOException e)
        {
            t = "no file to delete?";
        }
        try
        {

            FileStream cachtest = new FileStream(KeyCacheFile, FileMode.OpenOrCreate);
            BinaryWriter bwtest = new BinaryWriter(cachtest);
            cachtest.Close();
            bwtest.Close();
        }
        catch (IOException e)
        {
            Console.WriteLine(e.Message + "\n Cannot create file.");

            //return sb;
        }
        vcachI = new FileStream(VarCacheFile, FileMode.OpenOrCreate);
        vbwI = new BinaryWriter(vcachI);
        vbrI = new BinaryReader(vcachI);
    }


    public string SubReportTitel(string Titel)
    {

        String Day = DateTime.Now.Day.ToString();
        String Month = DateTime.Now.Month.ToString();
        String Year = DateTime.Now.Year.ToString();
        String Hour = DateTime.Now.Hour.ToString();
        String Minute = DateTime.Now.Minute.ToString();


        String t = "SUB REPORT ; " + Titel;
        t += "   DATE & TIME ; " + Month + "-" + Day + "-" + Year + " " + Hour + "-" + Minute;


        //
        //outfrm.out.SelStart = Len(outfrm.out.text)
        return t;
    }


    public void Menu(int setting) { }


    public void MsgBox(string remark) { }
    
  

    public void cacheIniCheck()
    {

        saveStatus(statusFileName, "initialzing Cache"); 

        NcVc0H = 0; NcVcH = 0; NcVc1H = 0;
        for (int v0 = 1; v0 < Vc0 + 1; v0++) { if (NcVc0[v0] > NcVc0H) { NcVc0H = NcVc0[v0]; } }
        for (int v = 1; v < Vc + 1; v++) { if (NcVc[v] > NcVcH) { NcVcH = NcVc[v]; } }
        for (int v1 = 1; v1 < Vc1 + 1; v1++) { if (NcVc1[v1] > NcVc1H) { NcVc1H = NcVc1[v1]; } }


        if (NcVc0H >= maxCatC-1 || NcVcH >= maxCatC-1  || Vc0 >= maxVarC-1 || Vc >= maxVarC-1) { UseProbCache = true; } else { UseProbCache = false; }
        // USE KEY CACHES?
        if (Ns >= MaxNsC || Nr >= MeanNr * MaxNsC) { UseVarKeyCache = true; } else { UseVarKeyCache = false; }
        //if (UseProbCache) { UseVarKeyCache = true; } //else { UseVarKeyCache = false; }

        if (UseProbCache || UseVarKeyCache  ) { cacheIni(); UseCache = true; } else { UseCache = false; }


    }


    public void getCriteria()
    {


        //  x  SubReportID = br.ReadString();
        //  x  AutoSelection = br.ReadInt32();
        //     ThresHoldPercentageCriterium = br.ReadInt32();
        //  x   MissingsInclude = br.ReadInt32();
        //  x  CriteriumHandling = br.ReadInt32();
        //  x  ThresHoldCriterium = br.ReadInt32();
        //  x  DifferenceCriterium = br.ReadInt32();
        //  x  AutoSelection = br.ReadInt32();
        //  x   Iterations = br.ReadInt32();
        //  x  Linkmethod = br.ReadInt32();
        //   chi2WithinShow = br.ReadInt32();
        //   Chi2BetweenShow = br.ReadInt32();
        //   SelectedIdentifiersShow = br.ReadInt32();
        //   CategoryFrequenciesShow = br.ReadInt32();
        //   DistributionLikelihoodsShow = br.ReadInt32();
        //   DistributionLinkedRecordsNShow = br.ReadInt32();
        //   LinkedSetsShow = br.ReadInt32();
        //   GraphicShow = br.ReadInt32();
        //   DataDir = br.ReadString();
        //   BinDir = br.ReadString();
        //   ResultDir = br.ReadString();

        string[] co = new string[9];

        if (co[1] == "reporttitle")
        {
            SubReportID = SubReportTitel(co[2]);
        }

        if (co[1] == "missinfinclude")
        {
            MissingsInclude = 1;
        }

        if (co[1] == "linkvariables")
        {

            if (co[2] == "auto")
            {

                AutoSelection = 1;

            }
            if (co[2] == "manual")
            {

                AutoSelection = 0;

            }
        }

        if (co[1] == "linkmethod")
        {
            if (co[2] == "bayesian")
            {
                Linkmethod = 0;
            }
            if (co[2] == "chisquare")
            {
                Linkmethod = 1;
            }

        }

        if (co[1] == "linkcriterium")
        {
            if (co[2] == "bestfit")
            {
                CriteriumHandling = 1;
            }
            if (co[2] == "bestfitabovethreshold")
            {
                CriteriumHandling = 2;
            }
            if (co[2] == "fitsabovethreshold")
            {
                CriteriumHandling = 3;
            }
            if (co[2] == "fitsabovethreshold")
            {
                CriteriumHandling = 4;
            }
        }

        if (co[1] == "thresholdcriterium")
        {
            if (co[2] == "auto")
            {
                ThresHoldCriterium = -99;
            }
            if (co[2] == "manual")
            {
                ThresHoldCriterium = Convert.ToDouble(co[3]);
            }
        }

        if (co[1] == "differencecriterium")
        {
            if (co[2] == "auto")
            {
                DifferenceCriterium = -99;
                if (co[2] == "manual")
                {
                    DifferenceCriterium = Convert.ToDouble(co[3]);
                }
            }
        }
        if (co[1] == "iterate")
        {
            if (co[2] == "manual")
            {
                Iterations = Convert.ToInt16(co[3]);

            }
            if (co[2] == "auto")
            {


            }
        }


    }


    public void getCriteriaDefaults()
    {

        DataDir = "c:";
        BinDir = "c:";
        ResultDir = "c:";

        SubReportID = "";

        //PARAMETERS START CLASSIFICATION

        AutoSelection = 0;
        ThresHoldPercentageCriterium = 90;
        MissingsInclude = 1;

        //PARAMETERS

        CriteriumHandling = 3;
        ThresHoldCriterium = -99;
        DifferenceCriterium = -99;
        AutoSelection = 0;
        Iterations = 3;
        Linkmethod = 0;

        //OUTPUT
        chi2WithinShow = 0;
        Chi2BetweenShow = 1;

        SelectedIdentifiersShow = 1;
        CategoryFrequenciesShow = 0;
        DistributionLikelihoodsShow = 0;
        DistributionLinkedRecordsNShow = 1;
        LinkedSetsShow = 0;
        GraphicShow = 1;

    }

    public void StandardsSave(string standardsFile)
    {   //         "C:\\standard.std";  

        int test;
        test = 24682468;

        try
        {
            BinaryWriter bw = new BinaryWriter(new FileStream(standardsFile,
             FileMode.Create));
        }
        catch (IOException e)
        {
            Console.WriteLine(e.Message + "\n Cannot create file.");
            return;
        }
        try
        {

            BinaryWriter bw = new BinaryWriter(new FileStream(standardsFile,
             FileMode.Create));

            bw.Write(Vc0);

            bw.Write(test);
            bw.Write(DataDir);
            bw.Write(BinDir);
            bw.Write(ResultDir);

            // PARAMETERS START CLASSIFICATION

            bw.Write(AutoSelection);
            bw.Write(ThresHoldPercentageCriterium);
            bw.Write(MissingsInclude);

            // PARAMETERS

            bw.Write(CriteriumHandling);
            bw.Write(ThresHoldCriterium);
            bw.Write(DifferenceCriterium);
            bw.Write(AutoSelection);
            bw.Write(Iterations);
            bw.Write(Linkmethod);

            // OUTPUT

            bw.Write(chi2WithinShow);
            bw.Write(Chi2BetweenShow);

            bw.Write(SelectedIdentifiersShow);
            bw.Write(CategoryFrequenciesShow);
            bw.Write(DistributionLikelihoodsShow);
            bw.Write(DistributionLinkedRecordsNShow);
            bw.Write(LinkedSetsShow);
            bw.Write(GraphicShow);

            bw.Write(Delimeter);
            bw.Close();
        }
        catch (IOException e)
        {
            Console.WriteLine(e.Message + "\n Cannot read from file.");
            return;
        }
    }

    public void StandardsGet(string standardsFile)
    {    //         "C:\\standard.std";  
        int test = 0;



        try
        {
            BinaryReader br = new BinaryReader(new FileStream(standardsFile, FileMode.Open));
        }
        catch (IOException e)
        {
            Console.WriteLine(e.Message + "\n Cannot create file.");
            return;
        }
        try
        {
            BinaryReader br = new BinaryReader(new FileStream(standardsFile, FileMode.Open));
            //        i = br.ReadInt32();        
            //        d = br.ReadDouble();            
            //        b = br.ReadBoolean();             
            //        s = br.ReadString();

            test = br.ReadInt32();

            //if ((test != 24682468))
            //{
            //    Defaults();
            //    StandardsSave("C:\\standard.std");
            //    return;
            //}

            DataDir = br.ReadString();
            BinDir = br.ReadString();
            ResultDir = br.ReadString();

            // PARAMETERS START CLASSIFICATION

            AutoSelection = br.ReadInt32();
            ThresHoldPercentageCriterium = br.ReadInt32();
            MissingsInclude = br.ReadInt32();

            // PARAMETERS

            CriteriumHandling = br.ReadInt32();
            ThresHoldCriterium = br.ReadInt32();
            DifferenceCriterium = br.ReadInt32();
            AutoSelection = br.ReadInt32();
            Iterations = br.ReadInt32();
            Linkmethod = br.ReadInt32();

            // OUTPUT

            chi2WithinShow = br.ReadInt32();
            Chi2BetweenShow = br.ReadInt32();

            SelectedIdentifiersShow = br.ReadInt32();
            CategoryFrequenciesShow = br.ReadInt32();
            DistributionLikelihoodsShow = br.ReadInt32();
            DistributionLinkedRecordsNShow = br.ReadInt32();
            LinkedSetsShow = br.ReadInt32();
            GraphicShow = br.ReadInt32();

            Delimeter = br.ReadString();

            br.Close();
        }
        catch (IOException e)
        {
            Console.WriteLine(e.Message + "\n Cannot read from file.");
            return;
        }


    }

    public void SetsRandom(Int64 NsIn, Int16 Vin,Int16 Cin)
    {

        Random   rnd = new   Random();
       
        Ns = NsIn;
        if (Ns < 500) { Ns = 500; }

        //ReDim rr(Ns)
        //ReDim Pr(Ns)
        //ReDim Hscore(1 To Ns) 'As Integer

        Nr = 0;
         
        Kc = 2;
        Vc0 = Vin;
        Vc = Convert.ToInt16(Vc0 + 1);
        Vc1 = 1;

        Int16[] w = new Int16[maxVar];
        // ReDim w(Vc0)
        // ReDim NcVc0(Vc0)
        // ReDim NcVc(Vc)
        // if (Vc1 > 0) {ReDim NcVc1(Vc1)

        // ReDim RespNr0(1 To Ns)
        // ReDim RespNr(1 To Ns * 4)
        // ReDim SetKey0(1 To Ns, 1 To Kc)
        // ReDim SetKey(1 To Ns * 4, 1 To Kc)
        // ReDim SetVar0(1 To Ns, Vc0)
        // ReDim SetVar(1 To Ns * 4, 1 To Vc)
        // if (Vc1 > 0) {ReDim SetVar1(1 To Ns * 4, 1 To Vc1)

        // ReDim BlokLabels(1 To Kc)
        // ReDim Var0Labels(1 To Vc0)
        // ReDim VarLabels(1 To Vc)
        // if (Vc1 > 0) {ReDim var1labels(1 To Vc1)

        // ReDim missing0(Vc0)
        // ReDim missing(Vc)
        // if (Vc1 > 0) {ReDim missing1(Vc1)

        

      
 

        for (int v = 1; v < Kc + 1; v++)
        {

            BlokLabels[v] = "Blok" + v.ToString();
        }
        for (int v0 = 1; v0 < Vc0 + 1; v0++)
        {

            Var0Labels[v0] = "InDep" + v0.ToString();
            missing0[v0] = 1;
        }
        for (int v = 1; v < Vc + 1; v++)
        {

            VarLabels[v] = "Dep" + v.ToString();
            missing[v] = 1;
        }
        for (int v1 = 1; v1 < Vc1 + 1; v1++)
        {
            Var1Labels[v1] = "Sing" + v1.ToString();
            missing1[v1] = 1;
        }

        for (int v = 1; v < Vc0 + 1; v++)
        {
            NcVc0[v] = Cin;
        }

        for (int v = 1; v < Vc + 1; v++)
        {
            NcVc[v] = Convert.ToInt16(Cin+1);    //altijd 1 hoger !! : Convert.ToInt16(rnd.Next(1, 2))) geeft altijd 1 extra categorie.. (dan alleen 1 toevoegen)
        }
        NcVc[Vc] = Convert.ToInt16(NcVc0[Vc0] + 1);  //correction..
        for (int v1 = 1; v1 < Vc1 + 1; v1++)
        {
            NcVc1[v1] = 2;
        }
      
         
        cacheIniCheck();

     Int64 posVarsV = 0; Int64 posVarsK = 0; Int64 PrNow = 0;
        string respNr0Old = "";
        for (Int64 s = 1; s <= Ns + 1; s++)
        {
            if (Math.Round(Convert.ToSingle(s) / 1000) == Convert.ToSingle(s) / 1000) { saveStatus(statusFileName, "creating random record: " + s.ToString()); }
            byte rnNow =  Convert.ToByte(rnd.Next(2, 7));
            //Pr[s] = Pr[s - 1];
           
            Int16[,] SRdata = new Int16[Vin+Vin+1, Cin+1];
            string[] respnrI = new string[byte.MaxValue];
            long LongNr = (long)((rnd.NextDouble() * 2.0 - 1.0) * long.MaxValue);
            respNr0Old = LongNr.ToString().Trim();

            respnrI[0] = respNr0Old; ;
            if (!UseVarKeyCache) { RespNr0[s] = respNr0Old; }
            //if (UseKeyCache) { posS0 = KeyCachSaveValue0(kcach0, kbw0, s, posS0, respNr0Old); }

            for (int k = 1; k < Kc + 1; k++)
            {
                //                SetKey0[s, k] = s;

            }
            for (int v0 = 1; v0 < Vc0 + 1; v0++)
            {

                w[v0] = Convert.ToInt16(rnd.Next(1, NcVc0[v0] + 1)); // Convert.ToInt32(Rnd() * NcVc0[v0]) + 1;
                //SetVar0[s, v0] = w[v0];
                SRdata[0, v0] = w[v0];
            }
            //double rrnd = rnd.Next(0, 1);
            //SetVar[Pr[s] + 1, 1] = Convert.ToInt16(rnd.Next(1, NcVc[1] + 1));
            SRdata[1, 1] = Convert.ToInt16(rnd.Next(1, NcVc[1] + 1));
            for (int v = 2; v < Vc + 1; v++)
            {
                //SetVar[Pr[s] + 1, v] = Convert.ToInt16(w[v - 1] + 1);
                SRdata[1, v] = Convert.ToInt16(w[v - 1] + 1);
            }

            //let op! aparte cache  !!
            respnrI[1] = respNr0Old; ;
            if (!UseVarKeyCache) { RespNr[PrNow + 1] = respNr0Old; }
            //if (UseKeyCache) { posS = KeyCachSaveValue(kcach, kbw, s, posS, respNr0Old); }

            //for (int r = 1; r < rnNow + 1; r++)
            //{

            //    for (int k = 1; k < Kc + 1; k++)
            //    {
                    //                    SetKey[Pr[s] + r, k] = SetKey0[s, k];
            //    }
            //}

            for (int r = 2; r < rnNow + 1; r++)
            {
                //RespNr[Pr[s] + r] = rnd.Next(1, Ns).ToString();
                respnrI[r] = respNr0Old; ;
                if (!UseVarKeyCache) { RespNr[PrNow + r] = respNr0Old; }
                //if (UseVarKeyCache) { posS = KeyCachSaveValue(kcach, kbw, s, posS, respNr0Old); }
                for (int v = 1; v < Vc + 1; v++)
                {
                    //SetVar[Pr[s] + r, v] = Convert.ToInt16(rnd.Next(1, NcVc[v]));  //= Convert.ToInt32(Rnd() * NcVc[v]) + 1;
                    SRdata[r, v] = Convert.ToInt16(rnd.Next(1, NcVc[v]));
                }
            }
            for (int v1 = 1; v1 < Vc1 + 1; v1++)
            {
                for (int r = 1; r < rnNow + 1; r++)
                {
                    //SetVar1[Pr[s] + r, v1] = Convert.ToInt16(rnd.Next(1, NcVc1[v1] + 1));//= Convert.ToInt32(Rnd() * 2);
                    SRdata[r, Vc + v1] = Convert.ToInt16(rnd.Next(1, NcVc1[v1] + 1));
                }
            }
             

            for (int v0 = 1; v0 < Vc0 + 1; v0++)
            {
                freq0[v0, SRdata[0, v0]] += 1;

            }
            for (int r = 1; r < rnNow + 1; r++)
            {
                for (int v = 1; v < Vc + 1; v++)
                {
                    freq[v, SRdata[r, v]] += 1;
                }
                for (int v1 = 1; (v1 <= Vc1); v1++)
                {
                    freq1[v1, SRdata[r, Vc + v1]] += 1;
                }
            }


            //Memory or cache
            if (!UseVarKeyCache)
            {
                saveSetToArray(s, PrNow, rnNow, SRdata);
                Nrr[s] = rnNow;
            }

            if (UseVarKeyCache) {
                posVarsK = KeyCachSaveValueK(kcach, kbw, kcachI, kbwI, s, rnNow, posVarsK, respnrI);
                posVarsV = varCachSaveValueK(vcach, vbw, vcachI, vbwI, s, rnNow, posVarsV, SRdata);
            }
            //Pr[s+1]  =Pr[s] + rnNow;
            PrNow = PrNow + rnNow;
        }

          saveStatus(statusFileName, "getting frequencies");  

        for (int v0 = 1; v0 < Vc0 + 1; v0++)
        {
            for (int c = 1; c <= NcVc0[v0]; c++)
            {
                code0[v0, c] = "C" + c.ToString("0");
            }
        }
        for (int v = 1; v < Vc + 1; v++)
        {
            for (int c = 1; c <= NcVc[v]; c++)
            {
                code[v, c] = "C" + c.ToString("0");
            }
        }

        for (int v1 = 1; v1 < Vc1 + 1; v1++)
        {
            for (int c = 1; c <= NcVc1[v1]; c++)
            {
                code1[v1, c] = "C" + c.ToString("0");
            }
        }
        //frequencies();
 
       
    }

    public Boolean saveSetToArray(Int64 sNow, Int64 PrNow, Byte rNow, Int16[,] SRdata)
    {

        for (int v0 = 1; v0 < Vc0 + 1; v0++)
        {
            SetVar0[sNow, v0] = SRdata[0, v0];

        }
        for (int r = 1; r <= rNow; r++)
        {
            for (int v = 1; v <= Vc; v++)
            {
                SetVar[PrNow + r, v] = SRdata[r, v];
            }
            for (int v1 = 1; v1 <= Vc1; v1++)
            {
                SetVar1[PrNow + r, v1] = SRdata[r, Vc + v1];
            }
        }

        return true;
    }

    public Int64 countTotalCategories() {

        NcVc0H = 0; NcVcH = 0; NcVc1H = 0;
        for (int v0 = 1; v0 < Vc0 + 1; v0++) { if (NcVc0[v0] > NcVc0H) { NcVc0H = NcVc0[v0]; } }
        for (int v = 1; v < Vc + 1; v++) { if (NcVc[v] > NcVcH) { NcVcH = NcVc[v]; } }
        for (int v1 = 1; v1 < Vc1 + 1; v1++) { if (NcVc1[v1] > NcVc1H) { NcVc1H = NcVc1[v1]; } }

        long cn = 0; long cn_old = 0;
    
        for (int v0 = 1; v0 <= Vc0 ; v0++)
        {
            for (int v = 1; v <= Vc ; v++)
            {
                cn += cn_old;
                posLi[v0, v] = cn;
                cn_old = (NcVc0[v0]+1) * (NcVc[v]+1);  //because not starting at zero!!
            }
        }
        cn += cn_old;
        return cn;
        //Int64 i = posLi[v0, v] + SRdata[0, v0] * NcVc[v] + SRdata[r, v];
    }

    public StringBuilder Probabilities(int iter_n)
    {

        Int64 PrNow = 0;
        StringBuilder sb = new StringBuilder();
        string t = "";
       

        


        //t = "Total number of combined categories (all by all) : " + cn.ToString();
        //t += "\r\n";

     
        t = "Number of categories set 0" + Delimeter;

        for (int v0 = 1; v0 < Vc0 + 1; v0++) { t += NcVc0[v0]; t += Delimeter; }
        t += "\r\n";
        t = "Number of categories set 1" + Delimeter;
        for (int v = 1; v < Vc + 1; v++) { t += NcVc[v]; t += Delimeter; }
        t += "\r\n";
        t = "Number of singular categories set 1" + Delimeter;
        for (int v1 = 1; v1 < Vc1 + 1; v1++) { t += NcVc1[v1]; t += Delimeter; }
        t += "\r\n";

        sb.Append(t); t = "";

        if (UseProbCache == false)
        {
            UseProbCache = false;
            //New OR Clean Arrays

            //Array.Clear(PP, 0, PP.Length);
            //Array.Clear(PPn, 0, PP.Length);
            //Array.Clear(PPChi, 0, PPChi.Length);
            //Array.Clear(PPratio, 0, PPratio.Length);

            Array.Clear(sPP, 0, sPP.Length);
            Array.Clear(sPPn, 0, sPP.Length);
            Array.Clear(sPPChi, 0, sPPChi.Length);
            Array.Clear(sPPratio, 0, sPPratio.Length);
        }
        if (UseProbCache == true)
        {

            //TEST


            ////Int32[,,,] PP = new Int32[maxCatC, maxCatC, maxVarC, maxVarC];
            ////Int32[,,,] PPn = new Int32[maxCatC, maxCatC, maxVarC, maxVarC];
            ////public float[,,,] PPratio = new float[maxCatC, maxCatC, maxVarC, maxVarC]; //4,4,vc0 vc
            ////public float[,,,] PPChi = new float[maxCatC, maxCatC, maxVarC, maxVarC]; //4,4,vc0 vc

            //Int32[,,,] iePP = new Int32[Vc0, Vc1, NcVc0H + 1, NcVcH + 1];
            //Int32[,,,] iePPn = new Int32[Vc0, Vc1, NcVc0H + 1, NcVcH + 1];
            //float[,,,] iPPChi = new float[Vc0, Vc1, NcVc0H + 1, NcVcH + 1];
            //float[,,,] iPPratio = new float[Vc0, Vc1, NcVc0H + 1, NcVcH + 1];

            //byte[] testbytes = new byte[9999999];


            //File.WriteAllBytes("c:/del/fileName",  testbytes );

            //lcach.Position = 0 + 16 + 0 * Vc0 * Vc * NcVc0H * NcVc0H; //lbw.Write(iePP());
            //lcach.Position = 0 + 16 + 1 * Vc0 * Vc * NcVc0H * NcVc0H; //lbw.Write(iePP());
            //lcach.Position = 0 + 16 + 2 * Vc0 * Vc * NcVc0H * NcVc0H; //lbw.Write(iePP());
            //lcach.Position = 0 + 16 + 3 * Vc0 * Vc * NcVc0H * NcVc0H; //lbw.Write(iePP());

          


            //Int64 pos = 0 + 16 * Vc1 * NcVc1H + 64;
            //Int64 pos2 = pos + 0 + 16 * ((v0 - 1) * Vc * NcVc0H * NcVcH + (v - 1) * NcVc0H * NcVcH + (c0 - 1) * NcVcH + (c - 1)) + 4 * (valueType - 1);
            //cach.Position = pos2;
            //writer.Write(floatNow);
            //if (cach.Position > 0 + 64 * Vc1 * NcVc1H + 64 + Convert.ToInt64(64)) { cacheEnd(); }
            //writer.Write(Convert.ToInt16(floatNow));           
            //writer.Write(Convert.ToInt32(floatNow));
            //writer.Write(Convert.ToInt64(floatNow));


            //END TEST

            //PUT ZERO'S IN CACHE TO START WITH N=0   
            float Zero1 = 0;
            float Zero2 = 0;
            float Zero3 = 0;
            float Zero4 = 0;

            //for (int v0 = 1; v0 <= Vc0; v0++)
            //{
            //    for (int v = 1; v <= Vc; v++)
            //    {
            //        for (int c0 = 1; c0 <= NcVc0[v0]; c0++)
            //        {
            //            for (int c = 1; c <= NcVc[v]; c++)
            //            {
            //                ProbCachSaveValue(lcach, lbw, Zero1, 1, v0, v, 0, c0, c, 0);
            //                ProbCachSaveValue(lcach, lbw, Zero2, 2, v0, v, 0, c0, c, 0);
            //                ProbCachSaveValue(lcach, lbw, Zero3, 3, v0, v, 0, c0, c, 0);
            //                ProbCachSaveValue(lcach, lbw, Zero4, 4, v0, v, 0, c0, c, 0);
            //            }
            //        }
            //    }
            //}
        }
        //cacheEnd();

        Array.Clear(CHIx, 0, CHIx.Length);
        Array.Clear(Ratiox, 0, Ratiox.Length);
        Array.Clear(CHIp, 0, CHIx.Length);

        Int32[,] ePP = new Int32[NcVc1H + 1, Vc1 + 1];
        Int32[,] ePPn = new Int32[NcVc1H + 1, Vc1 + 1];

        Array.Clear(ePPratio, 0, ePPratio.Length);

        Int64 N = 0;
        Int64 Nn = 0;

       
        float observed = 0;float notobserved = 0;
        float expected = 0;

        int vrijheid = 0;

        IterNumber = iter_n;

        if (IterNumber == 0)
        {

            t = "Iteration Step 1: Likelihoods based on raw linked records";

            Menu(3);

            //  ReDim Hscore(Nr)

            //for (int s = 1; s < Ns + 1; s++)
            //{
            //    for (int r = 1; r < Nrr[s] + 1; r++)
            //    {
            //        Hscore[Pr[s] + r] = 1;
             //   }
            //}
            for (int v0 = 1; v0 < Vc0 + 1; v0++)
            {
                for (int v = 1; v < Vc + 1; v++)
                {
                    SelectVar[v0, v] = 1;
                }
            }
            for (int v1 = 1; v1 < Vc1 + 1; v1++)
            {
                SelectVar1[v1] = 1;
            }
        }

        else
        {
            t = "Iteration Step ; " + IterNumber.ToString();
        }


         

        // ReDim PP(NcVc0H, NcVcH, 1 To Vc0, 1 To Vc) As Single
        // ReDim PPn(NcVc0H, NcVcH, 1 To Vc0, 1 To Vc) As Single
        // ReDim PPChi(NcVc0H, NcVcH, 1 To Vc0, 1 To Vc)
        // ReDim PPratio(NcVc0H, NcVcH, 1 To Vc0, 1 To Vc)


      

        //CONSTRUCT CROSSTABLES OF PP and PPn (linked and not linked)  
         
        for (int s = 1; s < Ns + 1; s++)
        {
            if (Math.Round(Convert.ToSingle(s) / 1000) == Convert.ToSingle(s) / 1000) { saveStatus(statusFileName, "calculating probabilities: " + s.ToString()); }
            //Get data of one set         
            Int16[,] SRdata = new Int16[maxRperRecord, maxVar];
            if (!UseVarKeyCache)
            {              
                SRdata[0, 0] = Nrr[s];
                
                for (int v0 = 1; v0 < Vc0 + 1; v0++)
                {
                    SRdata[0, v0] = SetVar0[s, v0];
                }
                for (int r = 1; r <= SRdata[0, 0]  ; r++)
                {
                    for (int v = 1; v < Vc + 1; v++)
                    {
                        //SRdata[r, v] = SetVar[Pr[s] + r, v];
                        SRdata[r, v] = SetVar[PrNow + r, v];
                    }
                    for (int v1 = 1; v1 < Vc1 + 1; v1++)
                    {
                        //SRdata[r, Vc + v1] = SetVar1[Pr[s] + r, Vc + v1];
                        SRdata[r, Vc + v1] = SetVar1[PrNow + r, Vc + v1];
                    }
                }
            }
            if (UseVarKeyCache) { SRdata = varCachGetValueK(vcach, vbr, vcachI, vbrI, s); }  //NO NOG UITBREIDEN NAAR DE REST (EN HIERVOOR).
          

            //SET Hscore to zero if first run;
            if (IterNumber == 0)
            {
                //for (int r = 1; r < SRdata[0, 0] + 1; r++)   {   Hscore[Pr[s] + r] = 1;  }
                for (int r = 1; r < SRdata[0, 0] + 1; r++) { Hscore[PrNow + r] = 1; }
            }
            //Get scores of selected and non selected
            for (int r = 1; r <= SRdata[0, 0]; r++)
            {
                for (int v0 = 1; v0 < Vc0 + 1; v0++)
                {
                    for (int v = 1; v < Vc + 1; v++)
                    {
                        if (SelectVar[v0, v] == 1)
                        {

                            if (!UseProbCache)
                            {
                                if (testarray == 1)
                                {
                                    //if (Hscore[PrNow + r] == 1) { PP[SRdata[0, v0], SRdata[r, v], v0, v] += 1; }
                                    //if (Hscore[PrNow + r] == 0) { PPn[SRdata[0, v0], SRdata[r, v], v0, v] += 1; }
                                }
                                else
                                {
                                    Int64 i = posLi[v0, v] + SRdata[0, v0] * NcVc[v] + SRdata[r, v];
                                    if (Hscore[PrNow + r] == 1) { sPP[i, v0, v] += 1; }
                                    if (Hscore[PrNow + r] == 0) { sPPn[i, v0, v] += 1; }
                                }


                            }
                            if (UseProbCache)
                            {
                                //if (Hscore[Pr[s] + r] == 1)
                                if (Hscore[PrNow + r] == 1)
                                {
                                    float ppp = 1 + ProbCachGetValue(lcach, lbr, 1, v0, v, 0, SRdata[0, v0], SRdata[r, v], 0);
                                    ProbCachSaveValue(lcach, lbw, ppp, 1, v0, v, 0, SRdata[0, v0], SRdata[r, v], 0);
                                }
                                //if (Hscore[Pr[s] + r] == 0)
                                if (Hscore[PrNow + r] == 0)
                                {
                                    float pppn = 1 + ProbCachGetValue(lcach, lbr, 2, v0, v, 0, SRdata[0, v0], SRdata[r, v], 0);
                                    ProbCachSaveValue(lcach, lbw, pppn, 2, v0, v, 0, SRdata[0, v0], SRdata[r, v], 0);
                                }
                            }
                        }

                    }
                }
                for (int v1 = 1; v1 < Vc1 + 1; v1++)
                {
                    if (SelectVar1[v1] > 0)
                    {
                        //inform("Contructing Singular Table ; " + Var1Labels[v1]);
                        if (!UseProbCache)
                        {
                            //if (Hscore[Pr[s] + r] == 1) { ePP[SRdata[r, Vc + v1], v1] += 1; }
                            //if (Hscore[Pr[s] + r] == 0) { ePPn[SRdata[r, Vc + v1], v1] += 1; }
                            if (Hscore[PrNow + r] == 1) { ePP[SRdata[r, Vc + v1], v1] += 1; }
                            if (Hscore[PrNow + r] == 0) { ePPn[SRdata[r, Vc + v1], v1] += 1; }


                        }
                        if (UseProbCache)
                        {
                            //if (Hscore[Pr[s] + r] == 1)
                            if (Hscore[PrNow + r] == 1)
                            {
                                float ppp = 1 + ProbCachGetValue(lcach, lbr, 1, 0, 0, v1, 0, 0, SRdata[r, Vc + v1]);
                                ProbCachSaveValue(lcach, lbw, ppp, 1, 0, 0, v1, 0, 0, SRdata[r, Vc + v1]);
                            }
                            //if (Hscore[Pr[s] + r] == 0)
                                if (Hscore[PrNow + r] == 0)
                            {
                                float pppn = 1 + ProbCachGetValue(lcach, lbr, 2, 0, 0, v1, 0, 0, SRdata[r, Vc + v1]);
                                ProbCachSaveValue(lcach, lbw, pppn, 2, 0, 0, v1, 0, 0, SRdata[r, Vc + v1]);
                            }
                        }
                    }
                }
            }

            PrNow += SRdata[0, 0];
        }
        for (int v0 = 1; v0 < Vc0 + 1; v0++)
        {
            for (int v = 1; v < Vc + 1; v++)
            {
                if (SelectVar[v0, v] == 1)
                {
                    //COMPUTE
                    N = 0; Nn = 0;
                    Int64[] nr = new Int64[NcVc0[v0] + 1];
                    Int64[] nc = new Int64[NcVc[v] + 1];
                    //double[] Nnr = new double[NcVc0[v0] + 1];
                    //double[] Nnc = new double[NcVc[v] + 1];

                    for (int c0 = 1; c0 < NcVc0[v0] + 1; c0++)
                    {
                        for (int c = 1; c < NcVc[v] + 1; c++)
                        {
                            if (!UseProbCache)
                            {

                                if (testarray == 1)
                                {
                                    //N += PP[c0, c, v0, v];
                                    //Nn += PPn[c0, c, v0, v];

                                    //nr[c0] += PP[c0, c, v0, v];
                                    //nc[c] += PP[c0, c, v0, v];

                                }
                                else
                                {
                                    Int64 i = posLi[v0, v] + c0 * NcVc[v] + c;

                                    N += sPP[i, v0, v] += 1;
                                    Nn += sPPn[i, v0, v] += 1;

                                    nr[c0] += sPP[i, v0, v];
                                    nc[c] += sPP[i, v0, v];
                                }
                            }
                            if (UseProbCache)
                            {
                                Int32 ppp = Convert.ToInt32(ProbCachGetValue(lcach, lbr, 1, v0, v, 0, c0, c, 0));
                                Int32 pppn = Convert.ToInt32(ProbCachGetValue(lcach, lbr, 2, v0, v, 0, c0, c, 0));

                                N += ppp;
                                Nn += pppn;
                                nr[c0] += ppp;
                                nc[c] += ppp;
                            }
                        }
                    }

                    //COMPUTE CHI en RATIO
                    Ratiox[v0, v] = 1;
                    for (int c0 = 1; c0 < NcVc0[v0] + 1; c0++)
                    {
                        for (int c = 1; c < NcVc[v] + 1; c++)
                        {
                            float ppc = 1; ; float ppr = 1;

                            if (!UseProbCache)
                            {

                                if (testarray == 1)
                                {
                                    //observed = PP[c0, c, v0, v];
                                    //notobserved = PP[c0, c, v0, v]; 
                                }
                                else
                                {
                                    Int64 i = posLi[v0, v] + c0 * NcVc[v] + c;
                                    observed = sPP[i, v0, v];
                                    notobserved = sPP[i, v0, v];
                                }
                            }
                            if (UseProbCache)
                            {
                                observed = ProbCachGetValue(lcach, lbr, 1, v0, v, 0, c0, c, 0);
                                notobserved = ProbCachGetValue(lcach, lbr, 1, v0, v, 0, c0, c, 0);
                            }
                            if (N > 0)
                            {
                                expected = Convert.ToSingle((nr[c0] * nc[c]) / N);  
                            }
                              if (expected > 0)
                            {
                                ppc = Convert.ToSingle(Math.Pow((observed - expected), 2) / expected * Math.Sign(observed - expected));
                                CHIx[v0, v] += ppc;
                            }
                            if (expected > 0 && observed > 0)
                            {
                                ppr = observed / expected; Ratiox[v0, v] *= ppr;
                                if (IterNumber > 0 && Linkmethod == 0) {
                                    ppr = Convert.ToSingle((observed / N) / (notobserved / Nn));   //oplossing voor: DIT GAAT NIET GOED MET CACHE.. WORDT NU NIET MEEGENOMEN I.
                                }
                            }
                            else { ppr = 1; }
                            if (!UseProbCache)
                            {
                                if (testarray == 1)
                                {
                                    //PPChi[c0, c, v0, v] = ppc;
                                    //PPratio[c0, c, v0, v] = ppr;
                                }
                                else
                                {
                                    Int64 i = posLi[v0, v] + c0 * NcVc[v] + c;
                                    sPPChi[i, v0, v] = ppc;
                                    sPPratio[i, v0, v] = ppr;
                                }
                                
                            }
                            if (UseProbCache)
                            {
                                ProbCachSaveValue(lcach, lbw, ppc, 3, v0, v, 0, c0, c, 0);
                                ProbCachSaveValue(lcach, lbw, ppr, 4, v0, v, 0, c0, c, 0);
                            }
                        }
                    }
                    

                    //SELECT DEPENDENCY OR NOT
                    int f1;
                    int f2;

                    f1 = NcVc0[v0] - 1;
                    f2 = NcVc[v] - 1;

                    if (f1 * f2 > 10000)
                    {
                        vrijheid = 10000;
                    }
                    else
                    {
                        vrijheid = (NcVc0[v0] - 1) * (NcVc[v] - 1);
                    }
                    //                   CHI(1, vrijheid, CHIx[v0, v], CHIp[v0, v]);


                }
            }
        }

        //DIT GAAT NIET GOED MET CACHE.. WORDT NU NIET MEEGENOMEN II.
        for (int v1 = 1; v1 < Vc1 + 1; v1++)
        {
            if (SelectVar1[v1] > 0)
            {
 
                N = 0; Nn = 0;
             
                for (int c1 = 1; c1 < NcVc1[v1] + 1; c1++)
                {
                    if (!UseProbCache)
                    {
                        N += ePP[c1, v1];
                        Nn += ePPn[c1, v1];
                      
                    }
                    if (UseProbCache)
                    {
                        Int32 ppp = Convert.ToInt32(ProbCachGetValue(lcach, lbr, 1, 0, 0, v1, 0, 0, c1));
                        Int32 pppn = Convert.ToInt32(ProbCachGetValue(lcach, lbr, 2, 0, 0, v1, 0, 0, c1));

                        N += ppp;
                        Nn += pppn;
                       
                    }

                }
                

                for (int c1 = 1; c1 < NcVc1[v1] + 1; c1++)
                {
                    if (IterNumber > 0)
                    {
                        if (Linkmethod == 0)
                        {
                            if (!UseProbCache) { observed = ePP[c1, v1]; notobserved = ePPn[c1, v1]; }
                            if (UseProbCache) { observed = ProbCachGetValue(lcach, lbr, 1, 0, 0, v1, 0, 0, c1); notobserved = ProbCachGetValue(lcach, lbr, 1, 0, 0, v1, 0, 0, c1); }
                            if (observed > 0 && N > 0) { ePPratio[c1, v1] = Convert.ToSingle((observed / N) / (notobserved / Nn)); }
                        }

                    }
                }


            }
        }

        if (AutoSelection == 1)
        {
            AutoSelectVariables();
        }
        else
        {
            if (IterNumber == 0)
            {
                //   ReDim SelectVar(Vc0, Vc)
                //   ReDim SelectVar1(Vc1)
                for (int v0 = 1; v0 < Vc0 + 1; v0++)
                {
                    for (int v = 1; v < Vc + 1; v++)
                    {

                        SelectVar[v0, v] = 1;
                    }
                }
                for (int v1 = 1; v1 < Vc1 + 1; v1++)
                {
                    SelectVar1[v1] = 1;
                }
            }
        }



        // double yyy = 0;
        // double Nnn = 0;



     

        //CLOSE CACHE FILE     


        //ProbCachSave(LikelihoodsCach);

        //end new

        // ReDim Hscore(Nr)
        Array.Clear(Hscore, 0, Hscore.Length);

        //SHOW CHI2 TABLE within VARIABLES
        if ((Vc + Vc0 + Vc1) * (NcVc0H + NcVcH + NcVc1H) > 1000) { chi2WithinShow = 0; }
        if (chi2WithinShow == 1)
        {
            t = "";
            t += "N Within";
            t += "\r\n";
            sb.Append(t); t = "";
            for (int v0 = 1; v0 < Vc0 + 1; v0++)
            {

                t += "Independent ; " + Var0Labels[v0];
                for (int c0 = 1; c0 < NcVc0[v0] + 1; c0++)
                {
                    t += ",";
                }
                t += ",,";
            }
            t += "\r\n";
            sb.Append(t); t = ",";
            for (int v0 = 1; v0 < Vc0 + 1; v0++)
            {
                t += ",";
                for (int c0 = 1; c0 < NcVc0[v0] + 1; c0++)
                {
                    t += "c" + c0.ToString() + "(" + code0[v0, c0] + ")";
                    t += ",";
                }
                t += ",";
            }
            t += "\r\n";
            sb.Append(t); t = "";


            for (int v = 1; v <= Vc; v++)
            {
                for (int c = 1; c <= NcVc[v]; c++)
                {
                    for (int v0 = 1; v0 <= Vc0; v0++)
                    {
                        t += "dependent ; " + VarLabels[v];
                        t += ",";
                        t += "c" + c.ToString() + "(" + code0[v, c] + ")";
                        t += ",";
                        for (int c0 = 1; c0 <= NcVc0[v0]; c0++)
                        {
                            if (!UseProbCache)
                            {
                                if (testarray == 1)
                                {
                                    //t += PP[c0, c, v0, v].ToString("0", CultureInfo.InvariantCulture);
                                }
                                else
                                {
                                    Int64 i = posLi[v0, v] + c0 * NcVc[v] + c;  
                                    t += sPP[i, v0, v].ToString("0", CultureInfo.InvariantCulture);
                                }
                            }
                            if (UseProbCache)
                            {
                                t += (ProbCachGetValue(lcach, lbr, 1, v0, v, 0, c0, c, 0)).ToString();
                            }
                            t += ",";
                        }
                    }
                    t += "\r\n";
                    sb.Append(t); t = "";
                }
            }
            t += "\r\n"; sb.Append(t); t = "";



            t = "";
            if (Linkmethod == 0) { t += "Likelihood Ratio's Within"; }
            if (Linkmethod == 1) { t += "Chi Square Within"; } // ; Independent ; " + Var0Labels[v0] + "&& dependent ; " + VarLabels[v] + " ; p = " + (CHIp[v0, v]).ToString("0.00", CultureInfo.InvariantCulture) + "}";
            t += "\r\n";
            sb.Append(t); t = "";
            for (int v0 = 1; v0 < Vc0 + 1; v0++)
            {

                t += "Independent ; " + Var0Labels[v0];
                for (int c0 = 1; c0 < NcVc0[v0] + 1; c0++)
                {
                    t += ",";
                }
                t += ",,";
            }
            t += "\r\n";
            sb.Append(t); t = ",";
            for (int v0 = 1; v0 < Vc0 + 1; v0++)
            {
                t += ",";
                for (int c0 = 1; c0 < NcVc0[v0] + 1; c0++)
                {
                    t += "c" + c0.ToString() + "(" + code0[v0, c0] + ")";
                    t += ",";
                }
                t += ",";
            }
            t += "\r\n";
            sb.Append(t); t = "";


            for (int v = 1; v <= Vc; v++)
            {
                for (int c = 1; c <= NcVc[v]; c++)
                {
                    for (int v0 = 1; v0 <= Vc0; v0++)
                    {
                        t += "dependent ; " + VarLabels[v];
                        t += ",";
                        t += "c" + c.ToString() + "(" + code0[v, c] + ")";
                        t += ",";
                        for (int c0 = 1; c0 <= NcVc0[v0]; c0++)
                        {
                            if (!UseProbCache)
                            {
                                if (testarray == 1)
                                {
                                    //if (Linkmethod == 0) { t += PPratio[c0, c, v0, v].ToString("0.00", CultureInfo.InvariantCulture); }
                                    //if (Linkmethod == 1) { t += PPChi[c0, c, v0, v].ToString("0.00", CultureInfo.InvariantCulture); }
                                }
                                else
                                {
                                    Int64 i = posLi[v0, v] + c0 * NcVc[v] + c; 
                                    if (Linkmethod == 0) { t += sPPratio[i, v0, v].ToString("0.00", CultureInfo.InvariantCulture); }
                                    if (Linkmethod == 1) { t += sPPChi[i, v0, v].ToString("0.00", CultureInfo.InvariantCulture); }
                                }
                            }
                            if (UseProbCache)
                            {
                                if (Linkmethod == 0) { t += (ProbCachGetValue(lcach, lbr, 3, v0, v, 0, c0, c, 0)).ToString("0.00", CultureInfo.InvariantCulture); }
                                if (Linkmethod == 1) { t += (ProbCachGetValue(lcach, lbr, 4, v0, v, 0, c0, c, 0)).ToString("0.00", CultureInfo.InvariantCulture); }
                            }
                            t += ",";
                        }
                    }
                    t += "\r\n";
                    sb.Append(t); t = "";
                }
            }
            t += "\r\n"; sb.Append(t); t = "";

        }

        if (Chi2BetweenShow == 1)
        {
            inform("");
            t = "";
            if (Linkmethod == 0) { t += "Ratio''s between Dependent and Independ Identifiers"; };
            if (Linkmethod == 1) { t += "Chi Squares between Dependent and Independ Identifiers"; };
            t += ",";
            t += "Dependent";
            t += "\r\n";
            sb.Append(t); t = ",";
            for (int v = 1; v < Vc + 1; v++)
            {
                t += "dep " + v.ToString("0", CultureInfo.InvariantCulture) + ": (" + VarLabels[v] + ")";
                t += ",";
            }
            t += "\r\n";
            sb.Append(t); t = "";
            for (int v0 = 1; v0 < Vc0 + 1; v0++)
            {
                t += "dep " + v0.ToString("0", CultureInfo.InvariantCulture) + ": (" + Var0Labels[v0] + ")";
                for (int v = 1; v < Vc + 1; v++)
                {
                    t += ",";
                    if (Linkmethod == 0) { t += Ratiox[v0, v].ToString("0.00", CultureInfo.InvariantCulture); }
                    if (Linkmethod == 1)
                    {
                        t += CHIx[v0, v].ToString("0.00", CultureInfo.InvariantCulture);
                        if (CHIp[v0, v] < 0.05 && CHIp[v0, v] != 0)
                        {
                            if (CHIp[v0, v] < 0.001) { t += "**"; } else { t += "*"; }
                        }
                    }
                }
                t += "\r\n";
                sb.Append(t); t = "";
            }
            t += "\r\n"; sb.Append(t); t = "";
        }
        // ReDim PP(0, 0, 0, 0)

 
        return sb;

    }


    public void SetsGetDelimeted(string rawfileNow)
    {

        int pos = 0; int rpos = 0; Int64 posVarsV = 0; Int64 posVarsK = 0;

        //string[] data = File.ReadAllLines(rawfileNow, Encoding.Default);

        System.IO.StreamReader file = new System.IO.StreamReader(rawfileNow);
        //while((line = file.ReadLine()) != null)

        /////////////////  hetzelfde als voor setsgetdelimeted.. procedures omzetten

        char dChar = Delimeter[0];
        t = ""; rpos = 0;
        do
        {
            t = file.ReadLine(); pos += 1;
            //t = data[rpos]; rpos += 1;
        } while (t.Trim() == "");

        string[] words = t.Split(dChar);
        Ns = Convert.ToInt64(words[0]);
        Kc = Convert.ToInt16(words[1]);
        Vc0 = Convert.ToInt16(words[2]);
        Vc = Convert.ToInt16(words[3]);
        Vc1 = Convert.ToInt16(words[4]);



        //       ReDim rr(Ns)
        //       ReDim Pr(Ns)

        //       ReDim RespNr0(1 To Ns)
        //       ReDim RespNr(1 To Ns * 4)
        //       if (Kc > 0) {ReDim SetKey0(1 To Ns, 1 To Kc)
        //       if (Kc > 0) {ReDim SetKey(1 To Ns * 4, 1 To Kc)
        //       ReDim SetVar0(1 To Ns, 1 To Vc0)
        //       ReDim SetVar(1 To Ns * 4, 1 To Vc)
        //       if (Vc1 > 0) {ReDim SetVar1(1 To Ns * 4, 1 To Vc1)

        //       if (Kc > 0) {ReDim BlokLabels(1 To Kc)
        //       ReDim Var0Labels(1 To Vc0)
        //       ReDim VarLabels(1 To Vc)
        //       if (Vc1 > 0) {ReDim var1labels(1 To Vc1)

        //       ReDim missing0(Vc0)
        //       ReDim missing(Vc)
        //       if (Vc1 > 0) {ReDim missing1(Vc1)

        int i = 0; int r = 0; t = "";
        do
        {
            do { t = file.ReadLine(); pos += 1; } while (t.Trim() == "");
            i += 1;
            words = t.Split(dChar);
            BlokLabels[i] = words[0];
        } while (i < 2);
        i = 0;

        do
        {
            do { t = file.ReadLine(); pos += 1; } while (t.Trim() == "");
            i += 1;

            words = t.Split(dChar);
            Var0Labels[i] = words[0];
            if (t.Length > 1) { missingS0[i] =  words[1].Trim(); }
        } while (i < Vc0);
        i = 0;
        do
        {
            do { t = file.ReadLine(); pos += 1; } while (t.Trim() == "");
            i += 1;

            words = t.Split(dChar);
            VarLabels[i] = words[0];
            if (t.Length > 1) { missingS[i] =  words[1].Trim(); }
        } while (i < Vc);
        i = 0;

        if (Vc1 > 0) {
            do
            {
                do { t = file.ReadLine(); pos += 1; } while (t.Trim() == "");
                i += 1;

                words = t.Split(dChar);
                Var1Labels[i] = words[0];
                if (t.Length > 1) { missingS1[i] = words[1].Trim(); }
            } while (i < Vc1);
        }
        rpos = pos;

        ///////////////// vanaf hier hetzelfde als deze proc niet old..


        // check first number of categories and Ns and Nr

        string[,] SetVarI = new string[maxRperRecord, maxVar];
        Int16[,] SRdata = new Int16[maxRperRecord, maxVar];
        string[] respnrI = new string[byte.MaxValue];

        int s = 0; byte rnNow = 0;
        string RecordNr = "";
        i = 0;
        do
        {

            do { t = file.ReadLine(); if (t == null) { break; } } while (t.Trim() == "");
            if (t != null) { words = t.Split(dChar); }   
            if (RecordNr == words[0].Trim() || t == null) { i += 1; } else { i = 0; }
            if (i == 0)
            {
                
                if (s > 0) { SRdata = recodeVars(rnNow, SetVarI); }         
                SetVarI = new string[maxRperRecord, maxVar];
                s += 1; rnNow = 0;
                if (Math.Round(Convert.ToSingle(s) / 1000) == Convert.ToSingle(s) / 1000) { saveStatus(statusFileName, "reading block: " + s.ToString()); }
                RecordNr = words[0].Trim();

                //if (RecordNr == "808E3")
                //{
                //    var test = 0;
                //}

                    //RespNr0[s] = words[1].Trim();
                    for (int c = 1; c <= Kc; c++)
                {
                    //SetKey0[s, c] = Convert.ToInt32(words[c + 1]);
                }
                for ( int c = 1; c <= Vc0; c++)
                {
                    SetVarI[0,c] = words[c + Kc + 1];
                }
                //recodeV0(s, SetVarI, false);
            }
            if (i > 0 & i < 200)
            {
                r += 1; rnNow += 1;
                //RespNr[s] = words[1].Trim();  //RespNr[(Pr[s] + r)]
                //RespNr[r] = words[1].Trim();

                for (int c = 1; c <= Kc; c++)
                {
                    //  SetKey[r, c] = Convert.ToInt32(words[c + 1]);
                }
                for (int c = 1; c <= Vc; c++)
                {
                    SetVarI[i, c] = words[c + Kc + 1];
                }
                for (int c = 1; c <= Vc1; c++)
                {
                    SetVarI[i, c + Vc] = words[c + Kc + Vc + 1];
                }

            }
            else
            {
                var test = 0;
            }
           
           

        } while (t != null);


        Ns = s; Nr = r;

        string gresult = arraysIniData(Ns, Nr, maxVarC);

        //frequencies();

        cacheIniCheck();

        Array.Clear(NcVc0, 0, NcVc0.Length);
        Array.Clear(NcVc, 0, NcVc.Length);
        Array.Clear(NcVc1, 0, NcVc1.Length);

        Array.Clear(code0, 0, code0.Length);
        Array.Clear(code, 0, code.Length);
        Array.Clear(code1, 0, code1.Length);


        //then run again to read the data, NOW WITH CACHE IF NEEDED

        pos = 0;
        file = new System.IO.StreamReader(rawfileNow);

        //START POSITIE IS ONTHOUDEN VAN VORIGE KEER!!
        do { t = file.ReadLine(); pos += 1; } while (pos != rpos);

        s = 0;   rnNow = 0;
         RecordNr = "";
         i = 0; r = 0; t = "";
         Int64 PrNow = 0; Int64 PrNowOld = 0;
        do
        { 

            do { t = file.ReadLine(); if (t == null) { break; } } while (t.Trim() == "");
            if (t != null) { words = t.Split(dChar); } else {
                SRdata = recodeVars(rnNow, SetVarI);
                PrNow = PrNowOld; PrNowOld = r;
                Frequencies(rnNow, SRdata);
                if (!UseVarKeyCache) { saveSetToArray(s, PrNow, rnNow, SRdata); Nrr[s] = rnNow; }
                if (UseVarKeyCache)
                {
                    posVarsK = KeyCachSaveValueK(kcach, kbw, kcachI, kbwI, s, rnNow, posVarsK, respnrI);
                    posVarsV = varCachSaveValueK(vcach, vbw, vcachI, vbwI, s, rnNow, posVarsV, SRdata);
                }
            }
            //if (s == 11999) { int rrr = 1; }
            if (RecordNr == words[0].Trim()) { i += 1; } else { i = 0; }
            if (i == 0)
            {   //SAVE TO CHACHE OR TO ARRAY
                if (s > 0)
                {
                    SRdata = recodeVars(rnNow, SetVarI);
                    //Pr[s+1] = r;
                    PrNow = PrNowOld; PrNowOld = r; 
                    Frequencies(rnNow, SRdata);
                    if (!UseVarKeyCache) { saveSetToArray(s, PrNow, rnNow, SRdata); Nrr[s] = rnNow; }
                    if (UseVarKeyCache) {
                        posVarsK = KeyCachSaveValueK(kcach, kbw, kcachI, kbwI, s, rnNow, posVarsK, respnrI);
                        posVarsV = varCachSaveValueK(vcach, vbw, vcachI, vbwI, s, rnNow, posVarsV, SRdata);
                    }                    
                }  

                SetVarI = new string[maxRperRecord, maxVar];

                //SRdata = new Int16[maxRperRecord, maxVar];
                //END SAVE TO CHACHE OR TO ARRAY
                s += 1; rnNow = 0;
                RecordNr = words[0].Trim();

                //RespNr0[r] = words[1].Trim();

                if (!UseVarKeyCache) { RespNr0[s] = words[1].Trim(); }
                respnrI[0] = words[1].Trim();
                //if (UseKeyCache) { posS0 = KeyCachSaveValue0(kcach0, kbw0, s, posS0, words[1].Trim()); }
                for (int c = 1; c <= Kc; c++)
                {
                    //                    SetKey0[s, c] = Convert.ToInt32(words[c + 1]);
                }
                for (int c = 1; c <= Vc0; c++)
                {
                    SetVarI[0, c] = words[c + Kc + 1];
                }
                //recodeV0(s, SetVarI, false);
            }
            if (i > 0 & i < 200)
            {
                r += 1; rnNow += 1;       
                if (!UseVarKeyCache) { RespNr[r]= words[1].Trim();}
                respnrI[i] = words[1].Trim();
                //if (UseKeyCache) { posS = KeyCachSaveValue(kcach, kbw, s, posS, words[0].Trim()); }
                for (int c = 1; c <= Kc; c++)
                {
                    //                    SetKey[r, c] = Convert.ToInt32(words[c + 1]);
                }
                for (int c = 1; c <= Vc; c++)
                {
                    SetVarI[i, c] = words[c + Kc + 1];
                }
                for (int c =   1; c <= Vc1; c++)
                {
                    SetVarI[i, c+Vc] = words[c + Kc + Vc + 1];
                }
                
            }
            else
            {
                var test = 0;
            }


        } while (t != null);


        Ns = s; Nr = r;
        //frequencies();



        //  Close #3



        inform("");


    }

  
    public void SetsWriteDelimeted(string rawfileNow, string dChar)
    {
        
        System.IO.StreamWriter file = new System.IO.StreamWriter(rawfileNow);
         
        //FILE INFO
        t = Ns.ToString() + dChar + Kc.ToString() + dChar + Vc0.ToString() + dChar + Vc.ToString() + dChar + Vc1.ToString(); file.WriteLine(t);
        t = ""; file.WriteLine(t);
        for (int i = 1; i <= Kc; i++) { t = BlokLabels[i]; file.WriteLine(t); } t = ""; file.WriteLine(t);
        for (int i = 1; i <= Vc0; i++) { t = Var0Labels[i] + dChar + missingS0[i]; file.WriteLine(t); } t = ""; file.WriteLine(t);
        for (int i = 1; i <= Vc; i++) { t = VarLabels[i] + dChar + missingS[i]; file.WriteLine(t); } t = ""; file.WriteLine(t);
        for (int i = 1; i <= Vc1; i++) { t = Var1Labels[i] + dChar + missingS1[i]; file.WriteLine(t); } t = ""; file.WriteLine(t);
    
     
        //DATA
         
        if (!UseVarKeyCache)
        {
            Int64 PrNow = 0; 
            for (Int64 s = 1; s <= Ns; s++)
            {
                t = s.ToString() + dChar;
                t += RespNr0[s].ToString() + dChar.ToString(); ;
                for (int i = 1; i <= Kc; i++) { t += "sk" + i.ToString() + dChar.ToString(); ; }
                for (int i = 1; i <= Vc0; i++) { t += code0[i, SetVar0[s, i]] + dChar.ToString(); ; }
                file.WriteLine(t);
                for (Byte r = 1; r <= Nrr[s]; r++)
                {
                    t = s.ToString() + dChar.ToString(); ;
                    t += RespNr0[s].ToString() + dChar.ToString(); ;
                    for (int i = 1; i <= Kc; i++) { t += "sk" + i.ToString() + dChar; }
                    for (int i = 1; i <= Vc; i++) { t += code[i, SetVar[PrNow + r, i]] + dChar.ToString(); }
                    for (int i = 1; i <= Vc1; i++) { t += code1[i, SetVar1[PrNow + r, i ]] + dChar.ToString(); }
                    file.WriteLine(t);
                }
                t = ""; file.WriteLine(t);
                PrNow += Nrr[s];
            }
        }
         
        if (UseVarKeyCache) {
            Int16[,] SRdata = new Int16[maxRperRecord, maxVar];
            string[] respnrI = new string[byte.MaxValue];
            Int64 PrNow = 0; byte NrNow = 0;
            for (Int64 s = 1; s <= Ns; s++)
            {
                SRdata = varCachGetValueK(vcach, vbr, vcachI, vbrI, s);
                respnrI = KeyCachGetValueK(kcach, kbr, kcachI, kbrI, s);
                //string KeY0 = KeyCachGetValue0(kcach, kbr, s);
                t = s.ToString() + dChar.ToString(); ;
                //t += KeY0 + dChar;
                t += respnrI[0] + dChar.ToString(); ;
                for (int i = 1; i <= Kc; i++) { t += "sk" + i.ToString() + dChar.ToString(); ; }
                for (int i = 1; i <= Vc0; i++) { t += code0[i,SRdata[0, i]] + dChar.ToString(); ; }
                file.WriteLine(t);
                NrNow=Convert.ToByte(SRdata[0, 0]);
                for (Byte r = 1; r <= NrNow; r++)
                {
                    //string KeY = KeyCachGetValue(kcach, kbr, Pr[s] + r);    //KLOPT DIT??   Pr[s] + r)
                    t = s.ToString() + dChar.ToString(); ;
                    //t += KeY.ToString() + dChar;
                    t += respnrI[r] + dChar;
                    for (int i = 1; i <= Kc; i++) { t += "sk" + i.ToString() + dChar.ToString(); ; }
                    for (int i = 1; i <= Vc; i++) { t += code[i, SRdata[r, i]] + dChar.ToString(); ; }
                    for (int i = 1; i <= Vc1; i++) { t += code1[i,SRdata[r  ,Vc+i]] + dChar.ToString(); ; }
                    file.WriteLine(t);
                }
                t = ""; file.WriteLine(t);
                PrNow += NrNow;
            }
        }

        file.Close();

    }

    public void Frequencies(int RecordN, Int16[,] SRdata)
    {
        for (int v0 = 1; v0 < Vc0 + 1; v0++)
        {
            freq0[v0, SRdata[0, v0]] += 1;

        }
        for (int r = 1; r < RecordN + 1; r++)
        {
            for (int v = 1; v < Vc + 1; v++)
            {
                freq[v, SRdata[r, v]] += 1;
            }
            for (int v1 = 1; (v1 <= Vc1); v1++)
            {
                freq1[v1, SRdata[r, Vc + v1]] += 1;
            }
        }
    }

    public Int16[,] recodeVars(int RecordN, string[,] SetVarI)
    {

        Int16[,] SRdata = new Int16[maxRperRecord, maxVar];

        Int16 found = 0;
        for (int v0 = 1; v0 <= Vc0; v0++)
        {
            found = 0;

            for (Int16 c = 1; (c <= NcVc0[v0]); c++)
            {
                if ((SetVarI[0, v0] == code0[v0, c])) { found = c; break; }
            }
            if ((found == 0))
            {
                NcVc0[v0] += 1;
                found = NcVc0[v0];
                //if (NcVc0[v0] > Int16.MaxValue) { MsgBox(("to many categories : variable " + Var0Labels[v0])); return; }
                code0[v0, NcVc0[v0]] = SetVarI[0, v0];
            }

            SRdata[0, v0] = found;

        }
        for (int r = 1; r <= RecordN; r++)
        {
            for (int v = 1; v <= Vc; v++)
            {
                found = 0;

                for (Int16 c = 1; (c <= NcVc[v]); c++)
                {
                    if ((SetVarI[r, v] == code[v, c])) { found = c; break; }
                }
                if ((found == 0))
                {
                    NcVc[v] += 1;
                    found = NcVc[v];
                    //if (NcVc[v] > Int16.MaxValue) { MsgBox(("to many categories : variable " + VarLabels[v])); return; }
                    code[v, NcVc[v]] = SetVarI[r, v];

                }

                SRdata[r, v] = found;

            }
            for (int v1 = 1; v1 <= Vc1; v1++)
            {
                found = 0;

                for (Int16 c = 1; (c <= NcVc1[v1]); c++)
                {
                    if ((SetVarI[r, Vc + v1] == code1[v1, c])) { found = c; break; }
                }
                if ((found == 0))
                {
                    NcVc1[v1] += 1;
                    found = NcVc1[v1];
                    //if (NcVc1[v1] > Int16.MaxValue) { MsgBox(("to many categories : variable " + Var1Labels[v1])); return; }
                    code1[v1, NcVc1[v1]] = SetVarI[r, Vc + v1];

                }

                SRdata[r, Vc + v1] = found;
            }
             
        }

        return SRdata;
    }
   

    public StringBuilder OutputFileInfo(string FileName)
    {
        StringBuilder sb = new StringBuilder();

        if (FileName.Trim() == "") { FileName = "Randomly created data"; }
        t = "REPORT BASED ON FILE : " + FileName;
        t += "\r\n"; sb.Append(t); t = "";
        t = "Independent Record N : " + Ns.ToString("0", CultureInfo.InvariantCulture);
        t += "\r\n"; sb.Append(t); t = "";
        t = "Dependent Record N : " + Nr.ToString("0", CultureInfo.InvariantCulture);
        t += "\r\n"; sb.Append(t); t = "";
        t = "Independent Variables";

        for (int v0 = 1; (v0 <= Vc0); v0++)
        {
            t += ",";
            t += v0.ToString("0", CultureInfo.InvariantCulture) + " : " + Var0Labels[v0];
        }
        t += "\r\n"; sb.Append(t); t = "";
        t = "Dependent Variables";

        for (int v = 1; (v <= Vc); v++)
        {
            t += ",";
            t += v.ToString("0", CultureInfo.InvariantCulture) + " : " + VarLabels[v];
        }
        t += "\r\n"; sb.Append(t); t = "";
        t = "Singular Variables";
        if ((Vc1 == 0))
        {
            t += ",";
            t += "None";
        }
        for (int v1 = 1; (v1 <= Vc1); v1++)
        {
            t += ",";
            t += v1.ToString("0", CultureInfo.InvariantCulture) + " : " + Var1Labels[v1];
        }
        t += "\r\n"; sb.Append(t); t = "";
        t += "\r\n"; sb.Append(t); t = "";

        
        t += "\r\n"; sb.Append(t); t = "";
        t += "\r\n"; sb.Append(t); t = "";
        return sb;

    }


    public void LikelihoodsSave(string LikelihoodFile)
    {

        string FileFirst = "";


    

        try
        {
            BinaryWriter bw = new BinaryWriter(new FileStream(LikelihoodFile,
              FileMode.Create));
        }
        catch (IOException e)
        {
            Console.WriteLine(e.Message + "\n Cannot create file.");
            return;
        }
        try
        {

            BinaryWriter bw = new BinaryWriter(new FileStream(LikelihoodFile,
              FileMode.Create));

            bw.Write(Vc0);
            bw.Write(Vc);
            bw.Write(Vc1);

            bw.Write(CriteriumHandling);
            bw.Write(ThresHoldPercentageCriterium);
            bw.Write(ThresHoldCriterium);
            bw.Write(DifferenceCriterium);
            bw.Write(AutoSelection);
            bw.Write(Linkmethod);
            bw.Write(MissingsInclude);


            for (int v0 = 1; v0 <= Vc0; v0++)
            {
                for (int v = 1; (v <= Vc); v++)
                {
                    bw.Write(SelectVar[v0, v]);
                }
            }
            for (int v1 = 1; v1 <= Vc1; v1++)
            {
                bw.Write(SelectVar1[v1]);
            }
            for (int v0 = 1; v0 <= Vc0; v0++)
            {
                bw.Write(missing0[v0]);
            }
            for (int v = 1; v <= Vc; v++)
            {
                bw.Write(missing[v]);
            }
            for (int v1 = 1; v1 <= Vc1; v1++)
            {
                bw.Write(missing1[v1]);
            }
            for (int v0 = 1; v0 <= Vc0; v0++)
            {
                bw.Write(NcVc0[v0]);
            }
            for (int v = 1; v <= Vc; v++)
            {
                bw.Write(NcVc[v]);
            }
            for (int v1 = 1; v1 <= Vc1; v1++)
            {
                bw.Write(NcVc1[v1]);
            }
            for (int v0 = 1; v0 <= Vc0; v0++)
            {
                for (int v = 1; v <= Vc; v++)
                {
                    if ((SelectVar[v0, v] == 1))
                    {
                        for (int c0 = 1; (c0 <= NcVc0[v0]); c0++)
                        {
                            for (int c = 1; (c <= NcVc[v]); c++)
                            {
                                if (testarray == 1)
                                {
                                    //bw.Write(PPratio[c0, c, v0, v]);
                                }
                                else
                                {
                                    Int64 i = posLi[v0, v] + c0 * NcVc[v] + c;
                                    bw.Write(sPPratio[i, v0, v]);
                                }
                            }
                        }
                    }
                }
            }
            for (int v1 = 1; (v1 <= Vc1); v1++)
            {
                for (int c = 1; (c <= NcVc1[v1]); c++)
                {
                    bw.Write(ePPratio[c, v1]);
                }
            }
    
        }
        catch (IOException e)
        {
            Console.WriteLine(e.Message + "\n Cannot write to file.");
            return;

        }

        Console.ReadKey();

        //SAVE ALSO IN TEXT..            
        ResultFile = (FileFirst + ".dsc");

        inform("Saving Likelihoods");
        for (int v0 = 1; (v0 <= Vc0); v0++)
        {
            for (int v = 1; (v <= Vc); v++)
            {
                if ((SelectVar[v0, v] == 1))
                {
                    for (int c0 = 1; (c0 <= NcVc0[v0]); c0++)
                    {
                        for (int c = 1; (c <= NcVc[v]); c++)
                        {
                            t = "";
                            t = (t + Var0Labels[v0]);
                            t = (t + Delimeter);
                            t = (t + VarLabels[v]);
                            t = (t + Delimeter);
                            t = (t + code0[v0, c0]);
                            t = (t + Delimeter);
                            t = (t + code[v, c]);
                            t = (t + Delimeter);

                            if (testarray == 1)
                            {
                                //t = (t + PPratio[c0, c, v0, v].ToString("0", CultureInfo.InvariantCulture));
                            }
                            else
                            {
                                Int64 i = posLi[v0, v] + c0 * NcVc[v] + c;
                                t = (t + sPPratio[i, v0, v].ToString("0", CultureInfo.InvariantCulture));
                            }
                            //save to text file                                     
                        }
                    }
                }
            }
        }
        for (int v1 = 1; (v1 <= Vc1); v1++)
        {
            for (int c = 1; (c <= NcVc1[v1]); c++)
            {
                t = "";
                t += Var1Labels[v1];
                t += Delimeter;
                t += code1[v1, c];
                t += Delimeter;
                t += ePPratio[c, v1].ToString("0", CultureInfo.InvariantCulture);
                //save to text file  
            }
        }
    }


    public void LikelihoodsGet(string LikelihoodFile, string FileName)
    {

        try
        {
            BinaryReader br = new BinaryReader(new FileStream(LikelihoodFile,
               FileMode.Open));
        }
        catch (IOException e)
        {
            Console.WriteLine(e.Message + "\n Cannot open file.");
            return;
        }
        try
        {

            BinaryReader br = new BinaryReader(new FileStream(LikelihoodFile,
             FileMode.Open));
            //      i = br.ReadInt32();
            //      Console.WriteLine("Integer data: {0}", i);
            //      d = br.ReadDouble();
            //      Console.WriteLine("Double data: {0}", d);
            //      b = br.ReadBoolean();
            //      Console.WriteLine("Boolean data: {0}", b);
            //      s = br.ReadString();
            //      Console.WriteLine("String data: {0}", s);

            inform("Getting Likelihoods");

            int nVc0;
            int nVc;
            int nVc1;

            nVc0 = br.ReadInt32();
            nVc = br.ReadInt32();
            nVc1 = br.ReadInt32();

            if ((nVc0 != Vc0))
            {
                MsgBox("Number of Independent Variables in the Likelihood File is Inconsistent with then number of Independen" +
                    "t Variables in the System File");
                br.Close();
            }

            if ((nVc != Vc))
            {
                MsgBox("Number of dependent Variables in the Likelihood File is Inconsistent with then number of dependent Va" +
                    "riables in the System File");
                br.Close();
            }

            if ((nVc1 != Vc1))
            {
                MsgBox("Number of Singular Variables in the Likelihood File is Inconsistent with then number of Singular Vari" +
                    "ables in the System File");
                br.Close();
            }

            CriteriumHandling = br.ReadInt32();
            ThresHoldPercentageCriterium = br.ReadInt32();
            ThresHoldCriterium = br.ReadInt32();
            DifferenceCriterium = br.ReadInt32();
            AutoSelection = br.ReadInt32();
            Linkmethod = br.ReadInt32();
            MissingsInclude = br.ReadInt32();

            //        redim SelectVar;
            //        redim SelectVar1;
            //        redim NcVc0;
            //        redim NcVc;


            for (int v0 = 1; (v0 <= Vc0); v0++)
            {
                for (int v = 1; (v <= Vc); v++)
                {
                    SelectVar[v0, v] = br.ReadByte();
                }
            }
            for (int v1 = 1; (v1 <= Vc1); v1++)
            {
                SelectVar1[v1] = br.ReadByte();
            }
            for (int v0 = 1; (v0 <= Vc0); v0++)
            {
                missing0[v0] = br.ReadInt16();
            }
            for (int v = 1; (v <= Vc); v++)
            {
                missing[v] = br.ReadInt16();
            }
            for (int v1 = 1; (v1 <= Vc1); v1++)
            {
                missing1[v1] = br.ReadInt16();
            }
            NcVc0H = 0;
            for (int v0 = 1; (v0 <= Vc0); v0++)
            {
                NcVc0[v0] = br.ReadInt16();
                if ((NcVc0[v0] > NcVc0H))
                {
                    NcVc0H = NcVc0[v0];
                }
            }
            NcVcH = 0;
            for (int v = 1; (v <= Vc); v++)
            {
                NcVc[v] = br.ReadInt16();
                if ((NcVc[v] > NcVcH))
                {
                    NcVcH = NcVc[v];
                }
            }
            NcVc1H = 0;
            for (int v1 = 1; (v1 <= Vc1); v1++)
            {
                NcVc1[v1] = br.ReadInt16();
                if ((NcVc1[v1] > NcVc1H))
                {
                    NcVc1H = NcVc1[v1];
                }
            }
            //        redim PPratio;
            //        redim ePPratio;
            for (int v0 = 1; (v0 <= Vc0); v0++)
            {
                for (int v = 1; (v <= Vc); v++)
                {
                    if ((SelectVar[v0, v] == 1))
                    {
                        for (int c0 = 1; (c0 <= NcVc0[v0]); c0++)
                        {
                            for (int c = 1; (c <= NcVc[v]); c++)
                            {
                                if (testarray == 1)
                                {
                                    //PPratio[c0, c, v0, v] = br.ReadInt32();
                                }
                                else
                                {
                                    Int64 i = posLi[v0, v] + c0 * NcVc[v] + c;
                                    sPPratio[i, v0, v] = br.ReadInt32();
                                }
                            }
                        }
                    }
                }
            }
            for (int v1 = 1; (v1 <= Vc1); v1++)
            {
                for (int c = 1; (c <= NcVc1[v1]); c++)
                {
                    ePPratio[c, v1] = br.ReadInt32();
                }
            }
            br.Close();
        }
        catch (IOException e)
        {
            Console.WriteLine(e.Message + "\n Cannot read from file.");
            return;
        }


        IterNumber = 0;
        t = "{\\deff \\pard ";
        t = (t + "\\par \\par ");
        t = (t + ("{\\b \\fs24 SUB REPORT : " + SubReportID));
        String Day = DateTime.Now.Day.ToString();
        String Month = DateTime.Now.Month.ToString();
        String Year = DateTime.Now.Year.ToString();
        String Hour = DateTime.Now.Hour.ToString();
        String Minute = DateTime.Now.Minute.ToString();
        t += Month + Day + Year + Minute + Year;
        t = (t + "\\par \\par ");
        t = (t + ("{\\b \\fs20 Likelihoods from File : "
                    + (FileName + " }")));
        t = (t + "\\par \\par ");
        t = (t + "\\par \\par ");
        t = (t + "}");
        //                     ;
        //                     outfrm.out.SelStart = outfrm.out.text.Length;
        LikelihoodSource = 1;
        //LinkRecords(ThresHoldPercentageCriterium, LikelihoodSource, 0, Linkmethod,"");
        inform("");


    }


    public StringBuilder OutputFrequencies()
    {

        StringBuilder sb = new StringBuilder();

        string t = "Frequencies";
        sb.AppendLine(t);
        for (int v0 = 1; (v0 <= Vc0); v0++)
        {
            t = "Independent Variable : " + Var0Labels[v0].Trim();
            t += "\r\n"; sb.Append(t); t = "";
            t = "New code";
            t += ",";
            t += "Old code";
            t += ",";
            t += "Category N";
            t += "\r\n"; sb.Append(t); t = "";

            for (int c = 1; (c <= NcVc0[v0]); c++)
            {
                t = c.ToString("0", CultureInfo.InvariantCulture);
                t += ",";
                t += code0[v0, c].Trim();
                t += ",";
                t += freq0[v0, c].ToString("0", CultureInfo.InvariantCulture);
                if (missing0[v0] == c) { t += ",(USER MISSING)"; }
                t += "\r\n"; sb.Append(t); t = "";
            }
        }
        for (int v = 1; (v <= Vc); v++)
        {
            t = "Dependent Variable : " + VarLabels[v].Trim();
            t += "\r\n"; sb.Append(t); t = "";
            t = "New code";
            t += ",";
            t += "Old code";
            t += ",";
            t += "Category N";
            t += "\r\n"; sb.Append(t); t = "";
            for (int c = 1; (c <= NcVc[v]); c++)
            {
                t = c.ToString("0", CultureInfo.InvariantCulture);
                t += ",";
                t += code[v, c].Trim();
                t += ",";
                t += freq[v, c].ToString("0", CultureInfo.InvariantCulture);
                if (missing0[v] == c) { t += ",(USER MISSING)"; }
                t += "\r\n"; sb.Append(t); t = "";

            }
        }
        for (int v1 = 1; (v1 <= Vc1); v1++)
        {
            t = "Singular Variable : " + Var1Labels[v1].Trim();
            t += "\r\n"; sb.Append(t); t = "";
            t = "New code";
            t += ",";
            t += "Old code";
            t += ",";
            t += "Category N";
            t += "\r\n"; sb.Append(t); t = "";
            for (int c = 1; (c <= NcVc1[v1]); c++)
            {
                t = c.ToString("0", CultureInfo.InvariantCulture);
                t += ",";
                t += code1[v1, c].Trim();
                t += ",";
                t += freq1[v1, c].ToString("0", CultureInfo.InvariantCulture);
                if (missing0[v1] == c) { t += ",(USER MISSING)"; }
                t += "\r\n"; sb.Append(t); t = "";
            }
        }
        t += "\r\n"; sb.Append(t); t = "";
        inform("End Recoding Identifiers");
        return sb;
    }


 

    public void inform(string message) { }


    public void AutoSelectVariables()
    {
        double hc = 0;
        int hh;
        for (int v0 = 1; (v0 <= Vc0); v0++)
        {
            for (int v = 1; (v <= Vc); v++)
            {
                SelectVar[v0, v] = 0;
            }
        }
        for (int v0 = 1; (v0 <= Vc0); v0++)
        {
            // TODO: # ... Warning!!! not translated
            hh = 0;
            for (int v = 1; (v <= Vc); v++)
            {
                // If Chip[v0, v] < 0.0001 Then
                //  If Chip[v0, v] < hc Then hc = Chip[v0, v]: hh = v
                // End If
                if ((CHIx[v0, v] > 999))
                {
                    if ((CHIx[v0, v] < hc))
                    {
                        hc = CHIx[v0, v];
                    }
                    hh = v;
                }
            }
            SelectVar[v0, hh] = 1;
        }
        //  For v0 = 1 To Vc0
        //   For v = 1 To Vc
        //    If Chip[v0, v] < 0.0001 Then SelectVar[v0, v] = 1 Else SelectVar[v0, v] = 0
        //   Next v
        //  Next v0
    }

    public StringBuilder LinkRecords(double perc, int Lsource, int iter_n, int Linkmethod, System.IO.StreamWriter fileScores)
    {     

        StringBuilder sb = new StringBuilder();
 
        int Nident;
        Int64[] Nrecords = new Int64[Int16.MaxValue];

        Threshold = 0;
        double Difference;
        ThresHoldPercentageCriterium = perc;
        IterNumber = iter_n;
        LikelihoodSource = Lsource;
        if ((IterNumber == 0))
        {

            t = "Determine Start Classification";

            LikelihoodSource = 0;
        }
        t += "\r\n";
        sb.Append(t); t = "";
        //  COMPUTE CHI or RATIO per RECORD IN SET

        switch (Linkmethod)
        {
            case -1:
                t = "Linkage Options : Bayesian Linkage (obs/exp) ";
                break;
            case 0:
                t = "Linkage Options : Bayesian Linkage";
                break;
            case 1:
                t = "Linkage Options : Chi Square Linkage";
                break;
        }
        t += "\r\n";
        sb.Append(t); t = "";
        switch (CriteriumHandling)
        {
            case 1:
                t = "Linkage Method : Best Fit in BLock ";
                break;
            case 2:
                t = "Linkage Method : Best Fit in BLock Above Threshold";
                break;
            case 3:
                t = "Linkage Method : Fits in BLock Above Threshold";
                break;
            case 4:
                t = "Linkage Method :Fits in BLock Above Threshold Given Difference";
                break;
        }
        t += "\r\n";
        sb.Append(t); t = "";
        if ((ThresHoldCriterium == -99))
        {
            t = "Threshold Criterium: ,Automatic : "
                         + ThresHoldCriterium.ToString("0.00", CultureInfo.InvariantCulture) + " based on percentage : "
                         + ThresHoldPercentageCriterium.ToString("0.00", CultureInfo.InvariantCulture) + " resulting in threshold : "
                         + Threshold.ToString("0.00", CultureInfo.InvariantCulture) + " }";
        }
        else
        {
            t = "Threshold Criterium : ,User Specified : "
                        + ThresHoldCriterium.ToString("0.00", CultureInfo.InvariantCulture) + " }";
        }
        t += "\r\n"; sb.Append(t); t = "";
        if ((DifferenceCriterium == -99))
        {
            t = "Difference Criterium : Automatic : " + DifferenceCriterium.ToString("0.00", CultureInfo.InvariantCulture);
        }
        else
        {
            t = "Difference Criterium : User Specified : " + DifferenceCriterium.ToString("0.00", CultureInfo.InvariantCulture);
        }
        t += "\r\n"; sb.Append(t); t = "";
        if ((AutoSelection == 1))
        {
            t = "Selection of Identifiers : Automatic";
        }
        else
        {
            t = "Selection of Identifiers : User Specified";
        }
        t += "\r\n"; sb.Append(t); t = "";
        t += "\r\n"; sb.Append(t); t = "";

        if (SelectedIdentifiersShow == 1)
        {
            t = "Selected Independent Identifiers : "; t += ",";
            Nident = 0;
            for (int v0 = 1; (v0 <= Vc0); v0++)
            {
                for (int v = 1; (v <= Vc); v++)
                {
                    if ((SelectVar[v0, v] == 1))
                    {
                        Nident = (Nident + 1);
                        t += "Indep - Dep " + Nident.ToString("0", CultureInfo.InvariantCulture) + " :";
                        t += ",";
                        t += Var0Labels[v0] + " - " + VarLabels[v];

                    }
                }
                t += "\r\n"; sb.Append(t); t = "";
            }

            t = "Selected Dependent Identifiers : "; t += ",";
            for (int v1 = 1; (v1 <= Vc1); v1++)
            {
                if ((SelectVar1[v1] == 1))
                {
                    Nident = (Nident + 1);
                    t += "Singular " + Nident.ToString("0", CultureInfo.InvariantCulture) + " :";
                    t += ",";
                    t += Var1Labels[v1];
                }
            }
            t += "\r\n"; sb.Append(t); t = "";
            t += "\r\n"; sb.Append(t); t = "";
        }


       

        if (Linkmethod == 0)
        {

            Int64 PrNow = 0;
            for (int s = 1; (s <= Ns); s++)
            {

                if (Math.Round(Convert.ToSingle(s) / 1000) == Convert.ToSingle(s) / 1000) { saveStatus(statusFileName, "linkmethod = Ratio, linking step: " + iter_n.ToString() + ", record: " + s.ToString()); }
                Int16[,] SRdata = new Int16[maxRperRecord, maxVar];
                if (!UseVarKeyCache)
                {
                    SRdata[0, 0] = Nrr[s];
                    for (int v0 = 1; v0 < Vc0 + 1; v0++)
                    {
                        SRdata[0, v0] = SetVar0[s, v0];

                    }
                    for (int r = 1; r <= SRdata[0, 0]; r++)
                    {
                        for (int v = 1; v < Vc + 1; v++)
                        {
                            SRdata[r, v] = SetVar[PrNow + r, v];
                        }
                        for (int v1 = 1; v1 < Vc1 + 1; v1++)
                        {
                            SRdata[r, Vc + v1] = SetVar1[PrNow + r, Vc + v1];
                        }
                    }
                }
                if (UseVarKeyCache) { SRdata = varCachGetValueK(vcach, vbr, vcachI, vbrI, s); }
                
                for (int r = 1; r <= SRdata[0, 0]; r++)
                {
                    score[(PrNow + r)] = 0;
                }
                for (int r = 1; (r <= SRdata[0, 0]); r++)
                {
                    for (int v0 = 1; (v0 <= Vc0); v0++)
                    {
                        //if (((SetVar0[s, v0] != missing0[v0]) || (MissingsInclude == 0)))
                        if (((SRdata[0, v0] != missing0[v0]) || (MissingsInclude == 0)))
                        {
                            for (int v = 1; (v <= Vc); v++)
                                if (((SRdata[r, v] != missing[v]) || (MissingsInclude == 0)))
                                {
                                    {
                                        if ((SelectVar[v0, v] == 1))
                                        {
                                            inform(("Computing Ratio\'s : Indepent " + v0.ToString("0", CultureInfo.InvariantCulture) + " with Dependent " + v.ToString("0.00", CultureInfo.InvariantCulture)));
                                            if (!UseProbCache)
                                            {
                                                if (testarray == 1)
                                                {
                                                    //score[(PrNow + r)] *= PPratio[SRdata[0, v0], SRdata[r, v], v0, v];
                                                }
                                                else
                                                {
                                                    Int64 i = posLi[v0, v] + SRdata[0, v0] * NcVc[v] + SRdata[r, v];
                                                    score[(PrNow + r)] *= sPPratio[i, v0, v];
                                                }
                                            }
                                            if (UseProbCache) { score[(PrNow + r)] *= ProbCachGetValue(lcach, lbr, 4, v0, v, 0, SRdata[0, v0], SRdata[r, v], 0); }
                                            
                                        }
                                    }
                                }
                        }
                    }

                    //fileScores.WriteLine(Addscore(s, r, PrNow + r, score[PrNow + r]));  //);
                }

                if ((IterNumber > 0))
                {
                    for (int v1 = 1; (v1 <= Vc1); v1++)
                    {
                        if (SelectVar1[v1] == 1)
                        {
                            inform("Computing Ratio\'s : Indepent "
                             + v1.ToString("0", CultureInfo.InvariantCulture)); // strange:??? + " with Dependent " + v0.ToString("0", CultureInfo.InvariantCulture));
                            // for (int s = 1; (s <= Ns); s++)
                            // {
                            for (int r = 1; r <= SRdata[0, 0]; r++)
                            {
                                if (SRdata[r, v1] != missing1[v1] || MissingsInclude == 0)
                                {

                                    score[(PrNow + r)] = score[PrNow + r] * ePPratio[SRdata[r, v1], v1];

                                     
                                }
                            }
                        }
                        //}
                    }
                }


                for (int r = 1; r <= SRdata[0, 0]; r++)
                {
                    if ((score[(PrNow + r)] > 0))
                    {
                        score[(PrNow + r)] = Convert.ToSingle(Math.Log(Convert.ToDouble(score[(PrNow + r)])));
                    }
                    else
                    {
                        score[(PrNow + r)] = -7;
                    }
                    if ((score[(PrNow + r)] < -7))
                    {
                        score[(PrNow + r)] = -7;
                    }

                    fileScores.WriteLine(Addscore(s, r, PrNow + r, score[PrNow + r]));  //);
                }
                PrNow += SRdata[0, 0];
            }
        }
        if (Linkmethod == 1)
        {
            Int64 PrNow = 0;
            for (int s = 1; (s <= Ns); s++)
            {
                if (Math.Round(Convert.ToSingle(s)/ 1000) == Convert.ToSingle(s) / 1000) { saveStatus(statusFileName, "linkmethod = Chi Square, linking step: " + iter_n.ToString() + ", record: " + s.ToString()); }
                Int16[,] SRdata = new Int16[maxRperRecord, maxVar];
                if (!UseVarKeyCache)
                {
                    SRdata[0, 0] = Nrr[s];
                    for (int v0 = 1; v0 < Vc0 + 1; v0++)
                    {
                        SRdata[0, v0] = SetVar0[s, v0];

                    }
                    for (int r = 1; r <= SRdata[0, 0]; r++)
                    {
                        for (int v = 1; v < Vc + 1; v++)
                        {
                            SRdata[r, v] = SetVar[PrNow + r, v];
                        }
                        for (int v1 = 1; v1 < Vc1 + 1; v1++)
                        {
                            SRdata[r, Vc + v1] = SetVar1[PrNow + r, Vc + v1];
                        }
                    }
                }
                if (UseVarKeyCache) { SRdata = varCachGetValueK(vcach, vbr, vcachI, vbrI, s); }
                for (int r = 1; r <= SRdata[0, 0]; r++)
                {
                    score[(PrNow + r)] = 0;
                }
                for (int r = 1; r <= SRdata[0, 0]; r++)
                {
                    for (int v0 = 1; (v0 <= Vc0); v0++)
                    {
                        for (int v = 1; (v <= Vc); v++)
                        {
                            if ((SelectVar[v0, v] == 1))
                            {
                                //inform("Computing Likelihoods : Indepent " + v0.ToString("0", CultureInfo.InvariantCulture) + " with Dependent " + v.ToString("0", CultureInfo.InvariantCulture));

                                if (!UseProbCache)
                                {
                                    if (testarray == 1)
                                    {
                                        //score[PrNow + r] += PPChi[SRdata[0, v0], SRdata[r, v], v0, v];
                                    }
                                    else
                                    {
                                        Int64 i = posLi[v0, v] + SRdata[0, v0] * NcVc[v] + SRdata[r, v];
                                        score[PrNow + r] += sPPChi[i, v0, v];
                                    }
                                }
                                if (UseProbCache)
                                {
                                    score[PrNow + r] += ProbCachGetValue(lcach, lbr, 3, v0, v, 0, SRdata[0, v0], SRdata[r, v], 0);
                                }
                               
                            }
                        }
                    }



                     fileScores.WriteLine(Addscore(s, r, PrNow + r, score[PrNow + r])) ;  //);
                  

                }
                PrNow += SRdata[0, 0];
            }
        }
        //fileScores.Close();

        //byte NcatGraph = 30;
        //double[] scores = new double[MaxGrCat];
         Nsmall = 0;
        int rh;
        double ll = 99999;
        double hh = -99999;
        double lh;

        Int64 PrNow2 = 0; Byte NrNow = 0;
        for (int s = 1; s <= Ns; s++)
        {
            if (!UseVarKeyCache) { NrNow = Nrr[s]; }
            if (UseVarKeyCache) { NrNow = varCachGetValueRK(vcach, vbr, vcachI, vbrI, s); }
            for (int r = 1; r <= NrNow; r++)
            {
                // 'If score(Pr[s] + r) <> -100 Then
                if (score[PrNow2 + r] > hh)
                {
                    hh = score[PrNow2 + r];
                }
                if (score[PrNow2 + r] < ll)
                {
                    ll = score[PrNow2 + r];
                }
                // 'Else
                // ' Nsmall = Nsmall + 1
                // 'End If
            }
            PrNow2 += NrNow;
        }
        
        inform("");
        // CATAGORY RANGE 1- 100
        int cNow = 0;
        lh = ((hh - ll) / NcatGraph);

  
        PrNow2 = 0;
        for (int s = 1; (s <= Ns); s++)
        {
            if (!UseVarKeyCache) { NrNow = Nrr[s]; }
            if (UseVarKeyCache) { NrNow = varCachGetValueRK(vcach, vbr, vcachI, vbrI, s); }
            for (int r = 1; r <= NrNow; r++)
            {
                // 'If score(Pr[s] + r) <> -100 Then
                //if (score[PrNow2 + r] == hh)
                //{
                    //Beep;
                //}
                if (lh > 0)
                {

                    cNow = Convert.ToInt32((score[PrNow2 + r] - ll) / lh);
                }
                scoresN[cNow] = scoresN[cNow] + 1;
                scoresV[cNow] += score[PrNow2 + r];
                // 'End If
            }
            PrNow2 += NrNow;
        }
        for (int c = 0; (c <= NcatGraph); c++)
        {
            if (scoresN[c] > 0) { scoresV[c] = scoresV[c] / scoresN[c]; } else { scoresV[c] = 0; };
        }
            // THRESHOLDCRITERIUM
            if ((ThresHoldCriterium == -99))
        {
            Threshold = ThresHoldScore(ThresHoldPercentageCriterium);
            //   For h = NcatGraph To 0 Step -1
            //    tel = tel + Fix(scores(h))
            //    If tel <= Ns * 0.95 Then Threshold = (h * lh) + ll
            //   Next h
        }
        else
        {
            Threshold = ThresHoldCriterium;
        }
        // DIFFERENCECRITERIUM
        if ((DifferenceCriterium == -99))
        {
            Difference = 0.0001;
        }
        else
        {
            Difference = DifferenceCriterium;
        }
        // CRITERIUMHANDLING
        Nselected = 0;
        double h = 0;
        switch (CriteriumHandling)
        {
            case 1:
                PrNow2 = 0;
                for (int s = 1; (s <= Ns); s++)
                {

                    if (!UseVarKeyCache) { NrNow = Nrr[s]; }
                    if (UseVarKeyCache) { NrNow = varCachGetValueRK(vcach, vbr, vcachI, vbrI, s); }
                    h = -99999;
                    rh = 0;
                    for (int r = 1; r <= NrNow; r++)   
                    {
                        if (score[PrNow2 + r] > h)
                        {
                            h = score[PrNow2 + r];
                            rh = r;
                        }

                    }
                    Hscore[PrNow2 + rh] = 1;
                    Nselected = (Nselected + 1);
                    PrNow2 += NrNow;
                }
                break;
            case 2:
                PrNow2 = 0;
                for (int s = 1; (s <= Ns); s++)
                {
                    if (!UseVarKeyCache) { NrNow = Nrr[s]; }
                    if (UseVarKeyCache) { NrNow = varCachGetValueRK(vcach, vbr, vcachI, vbrI, s); }
                    h = -99999;
                    rh = 0;
                    for (int r = 1; r <= NrNow; r++)
                    {
                        if ((score[PrNow2 + r] > h))
                        {
                            h = score[PrNow2 + r];
                            rh = r;
                        }

                    }
                    if (score[PrNow2 + rh] > Threshold)
                    {
                        Hscore[PrNow2 + rh] = 1;
                    }
                    Nselected = (Nselected + 1);
                    PrNow2 += NrNow;

                }
                break;
            case 3:
                PrNow2 = 0;
                for (int s = 1; (s <= Ns); s++)
                {
                    if (!UseVarKeyCache) { NrNow = Nrr[s]; }
                    if (UseVarKeyCache) { NrNow = varCachGetValueRK(vcach, vbr, vcachI, vbrI, s); }
                    h = -99999;
                    for (int r = 1; r <= NrNow; r++)
                    {
                        if (score[PrNow2 + r] > h && score[PrNow2 + r] > Threshold)
                        {
                            h = score[PrNow2 + r];
                            Hscore[PrNow2 + r] = 1;
                            Nselected = (Nselected + 1);
                        }

                    }
                    PrNow2 += NrNow;
                }
                break;
            case 4:
                PrNow2 = 0;
                for (int s = 1; (s <= Ns); s++)
                {
                    if (!UseVarKeyCache) { NrNow = Nrr[s]; }
                    if (UseVarKeyCache) { NrNow = varCachGetValueRK(vcach, vbr, vcachI, vbrI, s); }
                    h = -99999;
                    rh = 0;
                    for (int r = 1; r <= NrNow; r++)
                    {
                        if (score[PrNow2 + r] > h && score[PrNow2 + r] > Threshold)
                        {
                            h = score[PrNow2 + r];
                            rh = r;
                            Hscore[PrNow2 + r] = 1;
                            Nselected = (Nselected + 1);
                        }

                    }
                    for (int r = 1; r <= Nrr[s]; r++)
                    {
                        if (score[PrNow2 + rh] - score[PrNow2 + r] > DifferenceCriterium)
                        {
                            Hscore[PrNow2 + r] = 0;
                        }
                    }
                    PrNow2 += NrNow;
                }
                break;
        }
        // DISTRIBUTION OF NUMBER OF LINKED RECORDS PER SET
        PrNow2 = 0;
        for (int s = 1; (s <= Ns); s++)
        {
            if (!UseVarKeyCache) { NrNow = Nrr[s]; }
            if (UseVarKeyCache) { NrNow = varCachGetValueRK(vcach, vbr, vcachI, vbrI, s); }
            int Nin = 0;
            for (int r = 1; r <= NrNow; r++)
            {
                Nin = 0;
                if (Hscore[PrNow2 + r] == 1)
                {
                    Nin += 1;
                }
                Nrecords[Nin] += 1;
            }
            PrNow2 += NrNow;
        }
        if ((DistributionLikelihoodsShow == 1))
        {

            switch (Linkmethod)
            {
                case -1:
                    t = "Distribution of Likelihoods (obs/exp) : Link step : "
                                + IterNumber.ToString("0", CultureInfo.InvariantCulture) + " (N = "
                                + Nsmall.ToString("0", CultureInfo.InvariantCulture) + " < -99)";
                    break;
                case 0:
                    t = "Distribution of Likelihoods : Link step : "
                                + IterNumber.ToString("0", CultureInfo.InvariantCulture) + " (N = "
                                + Nsmall.ToString("0.00", CultureInfo.InvariantCulture) + " < -99)";
                    break;
                case 1:
                    t = "Distribution of CHI Squares : link step : " + IterNumber.ToString("0", CultureInfo.InvariantCulture);
                    break;
            }

            double HighNow = 0;
            for (int c = 0; (c <= NcatGraph); c++)
            {
                if (scoresN[c] > HighNow) { HighNow = scoresN[c]; }
            }
            for (int c = 0; (c <= NcatGraph); c++)
            {
                //t = (ll + c * lh).ToString("0.00", CultureInfo.InvariantCulture);
                t = scoresV[c].ToString("0.00", CultureInfo.InvariantCulture);
                t += ",";
                if (HighNow > 0) { for (int n = 1; n <= Convert.ToInt32((100 * scoresN[c] / HighNow)); n++) { t += "*"; } }
                t += "\r\n"; sb.Append(t); t = "";
            }

        }
        if ((LinkedSetsShow == 1))
        {


            switch (Linkmethod)
            {
                case -1:
                    t = "Likelihoods (obs/exp) : Link step : "
                       + IterNumber.ToString("0", CultureInfo.InvariantCulture) + " (N = "
                       + Nsmall.ToString("0", CultureInfo.InvariantCulture) + " < -99)";
                    break;
                case 0:
                    t = "Likelihood Ratios per record in recordset";
                    break;
                case 1:
                    t = "Chi Squares per record in recordset";
                    break;
            }

            for (int r = 1; (r <= 9); r++)
            {
                t += ",";
                t += "record " + r.ToString("0", CultureInfo.InvariantCulture);
            }
            t += "\r\n"; sb.Append(t); t = "";
            PrNow2 = 0;
            for (int s = 1; (s <= Ns); s++)
            {
                if (!UseVarKeyCache) { NrNow = Nrr[s]; }
                if (UseVarKeyCache) { NrNow = varCachGetValueRK(vcach, vbr, vcachI, vbrI, s); }
                // Exit For
                t = (t + ",");
                t = (t + ("set " + s.ToString("0", CultureInfo.InvariantCulture)));
                for (int r = 1; r <= NrNow; r++)
                {
                    t = (t + ",");
                    t += score[PrNow2 + r].ToString("0.00", CultureInfo.InvariantCulture);
                    if (Hscore[PrNow2 + r] == 1)
                    {
                        t = (t + "*");
                    }
                }
                t += "\r\n"; sb.Append(t); t = "";
                if (Convert.ToInt32(s / 20) == s / 20)
                {
                    inform(("Linking set number " + s.ToString("0", CultureInfo.InvariantCulture)));
                }
                if ((s > 59))
                {
                    break;
                }
                PrNow2 += NrNow;
            }
            t += "\r\n"; sb.Append(t); t = "";

        }
        // ' t = "{"
        // '   t = t & "\deff "
        // '   For c = 1 To 10
        // '    t = t & "\tx" & Format(2000 + (c - 1) * 1000)
        // '   Next c
        // '   If i = 0 Then
        // '    t = t & "{\b Likelihood Ratio's : Independent : " & Var0Labels(v0) & " and dependent : " & VarLabels(v) & " : p = " & Format(Chip[v0, v], " 0.0000") & "}"
        // '    t = t & "\par \par "



        if ((DistributionLinkedRecordsNShow == 1))
        {
            t = "Link step : " + IterNumber.ToString("0", CultureInfo.InvariantCulture) + " : ";

            switch (Linkmethod)
            {
                case -1:
                    t += "Likelihood(obs/exp) : Frequencies";
                    break;
                case 0:
                    t += "Likelihood Ratio\'s : Frequencies";
                    break;
                case 1:
                    t += "CHI Square values : Frequencies";
                    break;
            }
            t += "\r\n"; sb.Append(t); t = "";
            switch (Linkmethod)
            {
                case -1:
                    t += "Linked records per link set : (Criterium Likelihood Ratio > "
                                + Threshold.ToString("0.00", CultureInfo.InvariantCulture) + ") ";
                    break;
                case 0:
                    t += "Linked records per link set : (Criterium Likelihood Ratio > "
                                + Threshold.ToString("0.00", CultureInfo.InvariantCulture) + ") ";
                    break;
                case 1:
                    t += "Linked records per link set : (Criterium CHI Square > "
                                + Threshold.ToString("0.00", CultureInfo.InvariantCulture) + ") ";
                    break;
            }

             
            for (int r = 0; (r <= 9); r++)
            {
                t += ",";
                t += "N = " + r.ToString("0.00", CultureInfo.InvariantCulture);
                t += ",";
                t += (Nrecords[r] / Ns).ToString("0.00%", CultureInfo.InvariantCulture);

            }
            t += "\r\n"; sb.Append(t); t = "";
            t += "\r\n"; sb.Append(t); t = "";
        }
        if ((GraphicShow == 1))
        {
            //     GraphLikelihoods(scores(), ll, lh, Threshold, NcatGraph, Nsmall);
        }
 
        inform("");
        return sb;
    }


    public string Addscore(int s, long r, long rt, double score)
    {

        //char dChar = Delimeter[0];

        t = ""; //file.WriteLine(t);

        if (!UseVarKeyCache)
        {
            t += Delimeter;
            t += s.ToString("0", CultureInfo.InvariantCulture);
            t += Delimeter;
            t += r.ToString("0", CultureInfo.InvariantCulture);
            t += Delimeter;
            t += RespNr0[s];
            t += Delimeter;
            //t += RespNr[Pr[s] + r];
            t += RespNr[rt];
            t += Delimeter;
            t += score.ToString("0.00", CultureInfo.InvariantCulture);
            //t += Delimeter;
            //t = t + Hscore[Pr[s] + r].ToString("0.00", CultureInfo.InvariantCulture);
        }
        if (UseVarKeyCache)
        {
            string[] respnrI = new string[byte.MaxValue];
            respnrI = KeyCachGetValueK(kcach, kbr, kcachI, kbrI, s);

            t += Delimeter;
            t += s.ToString("0", CultureInfo.InvariantCulture);
            t += Delimeter;
            t += r.ToString("0", CultureInfo.InvariantCulture);
            t += Delimeter;
            t += respnrI[0];
            t += Delimeter;
            t += respnrI[r];
            t += Delimeter;
            t += score.ToString("0.00", CultureInfo.InvariantCulture);
            //t += Delimeter;
            //t = t + Hscore[Pr[s] + r].ToString("0.00", CultureInfo.InvariantCulture);
        }
        return t;
    }

    public double ThresHoldScore(double freq)
    {
        double ThresHoldScore2 = 0;
        int r;

        double N;
        int Nn = 0;
        int Nval;
        int i;
        Nval = 9000000;
        int[] valuesn = new int[MaxNr];
        inform("Computing Threshold Score");
        N = Ns * (freq / 100);
        for (r = 1; r <= Nr; r++)
        {
            i = Convert.ToInt32(score[r]) * 100;
            valuesn[i] += 1;
        }
        for (i = Nval; i <= (Nval * -1); i--)
        {
            Nn += valuesn[i];
            if ((Nn > N))
            {
                ThresHoldScore2 = (i / 1000);
            }
            break;
        }
        return ThresHoldScore2;
    }

    
    public Int64 KeyCachSaveValueK(FileStream cach, BinaryWriter writer, FileStream cachI, BinaryWriter writerI, Int64 s, Byte nr, Int64 posOld, string[] respnrI)
    {
        Int64 posNew = posOld + 1; //1 for nr

        for (byte r = 0; r < nr + 1; r++)
        {
            posNew += respnrI[r].Length + 2;

        }
        cach.Position = 8 * (s - 1);
        writer.Write(posOld);

        cachI.Position =    posOld ; //int.MaxValue + posOld;
        writerI.Write(nr);
        for (byte r = 0; r < nr + 1; r++)
        {
            writerI.Write(respnrI[r]);
        }
        return posNew;
    }

    public string[] KeyCachGetValueK(FileStream cach, BinaryReader reader, FileStream cachI, BinaryReader readerI, Int64 s)
    {
        string[] respnrI =new string[byte.MaxValue];
        cach.Position = 8 * (s - 1);
        Int64 pos = reader.ReadInt64();

        cachI.Position =  pos;
        Byte nr = readerI.ReadByte();
        for (byte r = 0; r < nr + 1; r++)
        {
           respnrI[r]=readerI.ReadString();
        }        
        return respnrI;
    }
   
    public Int64 varCachSaveValueK(FileStream cach, BinaryWriter writer, FileStream cachI, BinaryWriter writerI, Int64 s, Byte nr, Int64 posOld, Int16[,] SRdata)
    {
 
        Int64 posNew = posOld + 1 + 2 * Vc0 + 2 * nr * Vc + 4 * nr * Vc1;
        cach.Position = 8 * (s - 1);
        writer.Write(posOld);
//        varCachSaveValueI(vcachI, vbwI, s, nr, posOld, SRdata);
//        return posNew;
//    }
//    public void varCachSaveValueI(FileStream cach, BinaryWriter writer, Int64 s, Byte nr, Int64 posOld, Int16[,] SRdata)
//    {
       cachI.Position = Convert.ToInt64(posOld); //int.MaxValue + posOld;
        writerI.Write(nr);

        for (int v0 = 1; v0 < Vc0 + 1; v0++)
        {
            writerI.Write(SRdata[0, v0]);
        }
        for (byte r = 1; r < nr + 1; r++)
        {
            for (int v = 1; v < Vc + 1; v++)
            {
                writerI.Write(SRdata[r, v]);
            }
            for (int v1 = 1; v1 < Vc1 + 1; v1++)
            {
                writerI.Write(SRdata[r, Vc + v1]);
            }
        }
        return posNew; 
    }
     
    public Int16[,] varCachGetValueK(FileStream cach, BinaryReader reader, FileStream cachI, BinaryReader readerI, Int64 s)
    {
        Int16[,] SRdata = new Int16[maxRperRecord, maxVar];
        cach.Position = 8 * (s - 1);
        Int64 pos = reader.ReadInt64();   //1 te ver???
 //       Int16[,] SRdata2= varCachGetValueI(vcachI, vbrI, pos);     
 //       return SRdata2;
 //   }
 //   public Int16[,] varCachGetValueI(FileStream cach, BinaryReader reader, Int64 pos)
 //   {
 //       Int16[,] SRdata = new Int16[maxRperRecord, maxVar];
        cachI.Position =   pos;
        Byte nr = readerI.ReadByte();
        SRdata[0, 0] = nr;
        for (int v0 = 1; v0 < Vc0 + 1; v0++)
        {
            SRdata[0, v0] = readerI.ReadInt16();
        }
        for (byte r = 1; r < nr + 1; r++)
        {
            for (int v = 1; v < Vc + 1; v++)
            {
                SRdata[r, v] = readerI.ReadInt16();
            }
            for (int v1 = 1; v1 < Vc1 + 1; v1++)
            {
                SRdata[r, Vc + v1] = readerI.ReadInt16();
            }
        }

        return SRdata;
    }

   
    public Byte varCachGetValueRK(FileStream cach, BinaryReader reader, FileStream cachI, BinaryReader readerI, Int64 s)
    {
        //Int16[,] SRdata = new Int16[maxRperRecord, maxVar];

        cach.Position = 8 * (s - 1);
        Int64 pos = reader.ReadInt64();
        cachI.Position =  pos;
        Byte r = readerI.ReadByte();
        return r;
    }

    public void ProbCachSaveValue(FileStream cach, BinaryWriter writer, float floatNow, int valueType, int v0, int v, int v1, int c0, int c, int c1)
    {
        if (v == 0)
        {
            cach.Position = 0+ 16  * ((v1 - 1) * NcVc1H + (c1 - 1)) + 4 * (valueType - 1);
        }
        if (v1 == 0)
        {
            Int64 pos = 0 + 16 * Vc1 * NcVc1H + 64;
            Int64 pos2 = pos + 0 + 16 * ((v0 - 1) * Vc * NcVc0H * NcVcH + (v - 1) * NcVc0H * NcVcH + (c0 - 1) * NcVcH + (c - 1)) + 4 * (valueType - 1);
            cach.Position = pos2;
            writer.Write(floatNow);
            //if (cach.Position > 0 + 64 * Vc1 * NcVc1H + 64 + Convert.ToInt64(64)) { cacheEnd(); }
            //writer.Write(Convert.ToInt16(floatNow));           
            //writer.Write(Convert.ToInt32(floatNow));
            //writer.Write(Convert.ToInt64(floatNow));
        }
    }

    public float ProbCachGetValue(FileStream cach, BinaryReader reader, int valueType, int v0, int v, int v1, int c0, int c, int c1)
    {
        float floatNow = 1;
        if (v == 0)
        {
            cach.Position = 0 + 16 * ((v1 - 1) * NcVc1H + (c1 - 1)) + 4 * (valueType - 1);
        }
        if (v1 == 0)
        {
            Int64 pos = 0 + 16 * Vc1 * NcVc1H + 64;
            Int64 pos2 =    pos + 0 + 16 * ((v0 - 1) * Vc * NcVc0H * NcVcH + (v - 1) * NcVc0H * NcVcH + (c0 - 1) * NcVcH + (c - 1)) + 4 * (valueType - 1);
            cach.Position = pos2;
        }
        floatNow = reader.ReadSingle();
        return floatNow;
    }

    // moved to: class databaseFiles
   

    public void saveStatus(string fileNow, string status)
    {

        try
        {
            //System.IO.StreamWriter statusFile = new System.IO.StreamWriter(fileNow, false); //true to append data to the file; false to overwrite the file.
            //statusFile.WriteLine(status);
            //statusFile.Close();

        }
        catch (IOException e)
        {

        }
    }
      

}