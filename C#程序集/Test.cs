using System;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

[assembly:AssemblyKeyFile(@"C:\Users\10639\source\repos\Net期末项目\C#程序集\tanrui.keys")]
public class Test{
    public Test(){
        Console.WriteLine("Test C# DLL");
    }

    public int Add(int i){
        return i+i;
    }
}