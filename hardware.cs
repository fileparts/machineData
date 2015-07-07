using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Management;
using System.IO;

namespace socket_client
{
    class hardware
    {
        //getProcessorCores
        public static int getProcessorCores()
        {
            return Convert.ToInt32(Environment.ProcessorCount);
        }

        //getProcessorID
        public static string getProcessorID()
        {
            ManagementClass mc = new ManagementClass("win32_processor");
            ManagementObjectCollection moc = mc.GetInstances();

            String count = string.Empty;

            foreach (ManagementObject mo in moc)
            {
                count = mo.Properties["processorID"].Value.ToString();
                break;
            }
            return count;
        }

        //getProcessorSpeed
        public static string getProcessorSpeed()
        {
            double? GHz = null;
            using (ManagementClass mc = new ManagementClass("Win32_Processor"))
            {
                foreach (ManagementObject mo in mc.GetInstances())
                {
                    GHz = 0.001 * (UInt32)mo.Properties["CurrentClockSpeed"].Value;
                    break;
                }
            }

            return GHz.ToString() + " GHz";
        }
        //getAmountofRAM
        public static string getPhysicalMemory()
        {
            ManagementScope oMs = new ManagementScope();
            ObjectQuery oq = new ObjectQuery("SELECT Capacity FROM Win32_PhysicalMemory");
            ManagementObjectSearcher searcher = new ManagementObjectSearcher(oMs, oq);
            ManagementObjectCollection moc = searcher.Get();

            long MemSize = 0;
            long mCap = 0;

            foreach (ManagementObject mo in moc)
            {
                mCap = Convert.ToInt64(mo["Capacity"]);
                MemSize += mCap;
            }
            MemSize = (MemSize / 1024) / 1024;
            return MemSize.ToString() + " MB";
        }

        //getRAMSlots
        public static string getRAMSlots()
        {
            int MemSlots = 0;

            ManagementScope oMs = new ManagementScope();
            ObjectQuery oq = new ObjectQuery("SELECT MemoryDevices FROM Win32_PhysicalMemoryArray");
            ManagementObjectSearcher searcher = new ManagementObjectSearcher(oMs, oq);
            ManagementObjectCollection moc = searcher.Get();

            foreach (ManagementObject mo in moc)
                MemSlots = Convert.ToInt32(mo["MemoryDevices"]);

            return MemSlots.ToString();
        }

        //getDiskSpace
        public static string getDiskSpace()
        {
            long Available = 0;

            DriveInfo[] allDrives = DriveInfo.GetDrives();
            foreach (DriveInfo d in allDrives)
                if (d.IsReady == true)
                    Available += d.AvailableFreeSpace;

            return software.FormatBytes(Available);
        }
    }
}
