namespace CashSpawnEvent_CrashFix
{
    using System;
    using System.Diagnostics;
    using System.Text;

    public class Memory
    {
        #region Constants

        const int PROCESS_ALL_ACCESS = 0x1F0FFF;

        #endregion

        #region Variables

        private static string _Version = "";
        private static bool _Running;
        private static nint _BaseAddress;

        #endregion

        #region Public Methods

        /// <summary>
        ///     Returns if FiveM is Open.
        /// </summary>
        /// <param name="procOut">
        ///     The <see cref="Process"/> if exists.
        /// </param>
        public static bool IsFiveOpen(out nint handle)
        {
            bool _flag = false;
            handle = nint.Zero;

            foreach (var process in Process.GetProcesses())
            {
                string procName = process.ProcessName;
                if (procName.StartsWith("FiveM") && procName.Contains("GTA"))
                {
                    _flag = !_flag; 
                    handle = NativeImport.OpenProcess(PROCESS_ALL_ACCESS, false, process.Id);
                    _Version = Utils.Between(procName, "FiveM_", "_GTA");

                    if (process.MainModule != null)
                        _BaseAddress = process.MainModule.BaseAddress;
                }
            }

            _Running = _flag;
            return _flag;
        }

        public static bool ApplyFix(nint handle)
        {
            bool _flag = false;
            if (!_Running) return _flag;

            //sub_1413C60CC Crash Fix
            if (_Version == "b2372")
            {
                int bytesWritten = 0;
                byte[] buffer = 
                { 
                    0x75
                };

                _flag = NativeImport.WriteProcessMemory
                (
                    (int)handle,
                    Memory._BaseAddress + Offsets.patchByte,
                    buffer,
                    buffer.Length,
                    ref bytesWritten
                );
            }

            return _flag;
        }

        #endregion
    }
}
