using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.DirectoryServices;
using System.DirectoryServices.ActiveDirectory;
using System.Diagnostics;
using System.IO;

namespace ADSiteSubnetVerifier
{
    class ADSSVMain
    {
        static DirectorySearcher CreateDSSearcher()
        {
            // [Comment] Get local domain context
            string rootDSE;

            System.DirectoryServices.DirectorySearcher objrootDSESearcher = new System.DirectoryServices.DirectorySearcher();
            rootDSE = objrootDSESearcher.SearchRoot.Path;
            // [DebugLine]Console.WriteLine(rootDSE);

            // [Comment] Construct DirectorySearcher object using rootDSE string
            System.DirectoryServices.DirectoryEntry objrootDSEentry = new System.DirectoryServices.DirectoryEntry(rootDSE);
            System.DirectoryServices.DirectorySearcher objDSSearcher = new System.DirectoryServices.DirectorySearcher(objrootDSEentry);
            // [DebugLine]Console.WriteLine(objDSSearcher.SearchRoot.Path);
            return objDSSearcher;
        }

        static void PrintParameterSyntax()
        {
            // need to create proper parameter syntax output
        }

         static void LogToEventLog(string strAppName, string strEventMsg, int intEventType)
        {
            string sLog;

            sLog = "Application";

            if (!EventLog.SourceExists(strAppName))
                EventLog.CreateEventSource(strAppName, sLog);

            //EventLog.WriteEntry(strAppName, strEventMsg);
            EventLog.WriteEntry(strAppName, strEventMsg, EventLogEntryType.Information, intEventType);

        } // LogToEventLog

        static void Main(string[] args)
        {
            Forest objForest = Forest.GetCurrentForest();
            ReadOnlySiteCollection objROSC = objForest.Sites;

            foreach (ActiveDirectorySite objADSite in objROSC)
            {
                Console.WriteLine("Site: {0}", objADSite.Name);
                Console.WriteLine("Subnet(s) for this site:");
                foreach (ActiveDirectorySubnet objADSubnet in objADSite.Subnets)
                {
                    Console.WriteLine(objADSubnet.Name);
                }
                Console.WriteLine();
            }
        }
    }
}
