using System;
using System.Collections.Generic;
using System.Linq;
using System.Management;
using System.Text;

namespace UtilLib
{
    public static class SqlInstancesDiscovery
    {
        /// <summary>
        /// Enumerates all SQL Server instances on the machine.
        /// </summary>
        /// <returns></returns>
        public static string EnumerateSqlInstances()
        {
            var s = new StringBuilder();

            var correctNamespace = GetCorrectWmiNameSpace();

            if (string.Equals(correctNamespace, string.Empty))
            {
                return "There are no instances of SQL Server 2005 or SQL Server 2008 installed";
            }
            
            var query = "select * from SqlServiceAdvancedProperty where SQLServiceType = 1 and PropertyName = \'instanceID\'";
            
            var getSqlEngine = new ManagementObjectSearcher(correctNamespace, query);

            if (getSqlEngine.Get().Count == 0)
                return "There are no instances of SQL Server 2005 or SQL Server 2008 installed";

            Console.WriteLine("SQL Server database instances discovered :");

            s.AppendLine("Instance Name \t ServiceName \t Edition \t Version \t");
            
            foreach (var sqlEngine in getSqlEngine.Get().Cast<ManagementObject>())
            {
                var serviceName = sqlEngine["ServiceName"].ToString();
                var instanceName = GetInstanceNameFromServiceName(serviceName);
                var version = GetWmiPropertyValueForEngineService(serviceName, correctNamespace, "Version");
                var edition = GetWmiPropertyValueForEngineService(serviceName, correctNamespace, "SKUNAME");

                s.Append(String.Format("{0} \t", instanceName));
                s.Append(String.Format("{0} \t", serviceName));
                s.Append(String.Format("{0} \t", edition));
                s.AppendLine(String.Format("{0} \t", version));
            }

            return s.ToString();
        }

        /// <summary>
        /// Method returns the correct SQL namespace to use to detect SQL Server instances.
        /// </summary>
        /// <returns>namespace to use to detect SQL Server instances</returns>
        public static string GetCorrectWmiNameSpace()
        {
            var wmiNamespaceToUse = "root\\Microsoft\\sqlserver";
            var namespaces = new List<string>();
            try
            {
                // Enumerate all WMI instances of
                // __namespace WMI class.
                var nsClass =
                    new ManagementClass(
                    new ManagementScope(wmiNamespaceToUse),
                    new ManagementPath("__namespace"),
                    null);

                namespaces.AddRange(
                    nsClass
                    .GetInstances()
                    .Cast<ManagementObject>()
                    .Select(ns => ns["Name"].ToString())
                    );
            }
            catch (ManagementException e)
            {
                Console.WriteLine("Exception = " + e.Message);
            }
            if (namespaces.Count > 0)
            {
                if (namespaces.Contains("ComputerManagement10"))
                {
                    //use katmai+ namespace
                    wmiNamespaceToUse = wmiNamespaceToUse + "\\ComputerManagement10";
                }
                else if (namespaces.Contains("ComputerManagement"))
                {
                    //use yukon namespace
                    wmiNamespaceToUse = wmiNamespaceToUse + "\\ComputerManagement";
                }
                else
                {
                    wmiNamespaceToUse = string.Empty;
                }
            }
            else
            {
                wmiNamespaceToUse = string.Empty;
            }
            return wmiNamespaceToUse;
        }
        /// <summary>
        /// method extracts the instance name from the service name
        /// </summary>
        /// <param name="serviceName"></param>
        /// <returns></returns>
        public static string GetInstanceNameFromServiceName(string serviceName)
        {
            if (!string.IsNullOrEmpty(serviceName))
            {
                return 
                    string
                    .Equals(serviceName, "MSSQLSERVER", StringComparison.OrdinalIgnoreCase) 
                    ? serviceName 
                    : serviceName.Substring(serviceName.IndexOf('$') + 1, serviceName.Length - serviceName.IndexOf('$') - 1);
            }

            return string.Empty;
        }

        /// <summary>
        /// Returns the WMI property value for a given property name for a particular SQL Server service Name
        /// </summary>
        /// <param name="serviceName">The service name for the SQL Server engine serivce to query for</param>
        /// <param name="wmiNamespace">The wmi namespace to connect to </param>
        /// <param name="propertyName">The property name whose value is required</param>
        /// <returns></returns>
        public static string GetWmiPropertyValueForEngineService(string serviceName, string wmiNamespace, string propertyName)
        {
            var propertyValue = string.Empty;
            var query = String.Format("select * from SqlServiceAdvancedProperty where SQLServiceType = 1 and PropertyName = '{0}' and ServiceName = '{1}'", propertyName, serviceName);
            var propertySearcher = new ManagementObjectSearcher(wmiNamespace, query);

            foreach (var sqlEdition in propertySearcher.Get().Cast<ManagementObject>())
                propertyValue = sqlEdition["PropertyStrValue"].ToString();

            return propertyValue;
        }
    }
}
