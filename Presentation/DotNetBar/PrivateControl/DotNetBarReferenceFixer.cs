using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using System.Diagnostics;

namespace FISCA.Presentation.DotNetBar.PrivateControl
{
    static class DotNetBarReferenceFixer
    {
        private static bool _Fixited = false;
        public static void FixIt()
        {
            if ( _Fixited ) return;
            _Fixited = true;
            AppDomain.CurrentDomain.AssemblyResolve += new ResolveEventHandler(CurrentDomain_AssemblyResolve);
        }

        static System.Reflection.Assembly CurrentDomain_AssemblyResolve(object sender, ResolveEventArgs args)
        {
            bool firstOne = true;
            foreach ( StackFrame frame in ( new StackTrace() ).GetFrames() )
            {
                if ( firstOne )
                {
                    firstOne = false;
                    continue;
                }
                if ( frame.GetMethod().ReflectedType.Assembly.GetName().Name == "mscorlib" )
                    continue;
                if (  frame.GetMethod().ReflectedType.Assembly == Assembly.GetAssembly(typeof(DotNetBarReferenceFixer)) )
                {
                    System.Reflection.AssemblyName name = new System.Reflection.AssemblyName(args.Name);
                    if ( name.Name == "DevComponents.DotNetBar2" )
                    {
                        foreach ( var item in AppDomain.CurrentDomain.GetAssemblies() )
                        {
                            if ( item.GetName().Name == name.Name && item.GetName().Version >= name.Version )
                                return item;
                        }
                        return AppDomain.CurrentDomain.Load(Properties.Resources.DevComponents_DotNetBar2);
                    }
                }
                else
                    break;
            }
            return null;
        }
    }
}
