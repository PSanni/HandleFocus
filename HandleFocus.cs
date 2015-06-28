
/* 
   Class file to handle windows handle. 
 * Author: Sunny patel 
 * @sannikpatel
 */

using System;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;

namespace ForeGround
{
    class HandleFocus
    {
        [DllImport("User32.dll")]
        static extern int SetForegroundWindow(IntPtr hWnd);

        [DllImport("user32.dll")]
        internal static extern bool SendMessage(IntPtr hWnd, Int32 msg, Int32 wParam, Int32 lParam);
        static Int32 WM_SYSCOMMAND = 0x0112;
        static Int32 SC_RESTORE = 0xF120;

        /// <summary>
        /// Get windows focus by process(thread) name.
        /// </summary>
        /// <param name="name">Process name of window</param>
        public void Focus(String name) {

            if (Process.GetProcessesByName(name).Any())
            {
                Process proc = Process.GetProcessesByName(name).First();

                if (proc != null)
                {
                    try
                    {
                        //Takig Windows handle
                        IntPtr pointer = proc.MainWindowHandle;
                        SetForegroundWindow(pointer); // API call

                        SendMessage(pointer, WM_SYSCOMMAND, SC_RESTORE, 0); //Restore if minimized
                    }
                    catch (Exception e)
                    {
                        throw new HandleFocusException("Exception occured while focusing", e);
                    }
                }
                else {

                    throw new HandleFocusException("Windows Handle not found");
                }
           
            }
            else {

                throw new HandleFocusException("Given process not found in stack");
            }
        }

        /// <summary>
        /// Get windows focus by windows title
        /// </summary>
        /// <param name="title">Title of windows.</param>
        public void FocusByTitle(String title) {
            Process proc = ProcessByTitle(title);

            if (proc != null)
            {
                try
                {
                    //Taking windows Handle
                    IntPtr pointer = proc.MainWindowHandle;
                    SetForegroundWindow(pointer);

                    SendMessage(pointer, WM_SYSCOMMAND, SC_RESTORE, 0);//Restore if minimized
                }
                catch (Exception e)
                {
                    throw new HandleFocusException("Exception occured while focusing", e);
                }
            }
            else
            {

                throw new HandleFocusException("Windows Handle not found");
            }
        }

        
        private Process ProcessByTitle(string title) {
            Process[] List = Process.GetProcesses();
            foreach (Process p in List) {
                //Checking, if windows available
                if (p.MainWindowTitle == title) {
                    return p;
                }
            }
            return null;
        }
    }
 
    public class HandleFocusException : Exception 
    {
        public HandleFocusException() { }
        public HandleFocusException (string message):base(message){}
        public HandleFocusException(string message, Exception inner) : base(message, inner) { }
    }
}
