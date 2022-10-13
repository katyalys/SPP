using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace laba2var2New_spp
{
    public class OSHandle : IDisposable
    {
        private bool disposed = false;
        private IntPtr ptr;

        public OSHandle()
        {
            // Выделяет память из неуправляемой памяти процесса, используя указатель на заданное количество байтов.
            ptr = Marshal.AllocHGlobal(100);
        }

        public IntPtr Handle
        {
            get
            {
                if (!disposed)
                    return ptr;
                else
                    throw new ObjectDisposedException(ToString());
            }
            set { ptr = value; }
        }

        public void Dispose()
        {
            Dispose(disposing: true);
            // Сообщает среде CLR, что она на не должна вызывать метод завершения для указанного объекта.
            GC.SuppressFinalize(this);
        }

        // для неуправляемях ресурсов
        protected virtual void Dispose(bool disposing)
        {
            // Check to see if Dispose has already been called.
            if (!this.disposed)
            {
                if (disposing) { }

                //CloseHandle(ptr);
                //ptr = IntPtr.Zero;
                Marshal.FreeHGlobal(ptr);

                // Note disposing has been done.
                disposed = true;
            }
        }

        //[System.Runtime.InteropServices.DllImport("Kernel32")]
        //private extern static Boolean CloseHandle(IntPtr handle);

        //for unmanaged resources
        // avtomat
        //finalize
        ~OSHandle()
        {
            Dispose(disposing: false);
        }
    }
}
