// Author: Anthony anthonysam538
// Email: anthonysam538@csu.fullerton.edu
// Course: CPSC 223N
// Semester: Fall 2019
// Assignment #6
// Program Name: Flower

// Name of this file: FlowerMain.cs
// Purpose of this file:
// Purpose of this entire program: This program draws a flower.

// Source files in this program: FlowerForm.cs, FlowerMain.cs
// The source files in this program should be compiled in the order specified below in order to satisfy dependencies.
//  1. FlowerForm.cs compiles into library file FlowerForm.dll
//  2. FlowerMain.cs compiles and links with the dll file above to create Flower.exe
// Compile (and link) this file:
// mcs -r:System.Windows.Forms.dll -r:FlowerForm.dll -out:Flower.exe FlowerMain.cs
// Execute (Linux shell): ./Flower.exe

using System;
using System.Windows.Forms;

public class FlowerMain
{
  public static void Main()
  {
    System.Console.WriteLine("The flower program will begin now.");
    FlowerForm Flower_App = new FlowerForm();
    Application.Run(Flower_App);

    System.Console.WriteLine("The flower program has ended.");
  }
}
