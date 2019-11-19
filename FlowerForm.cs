// Author: Anthony anthonysam538
// Email: anthonysam538@csu.fullerton.edu
// Course: CPSC 223N
// Semester: Fall 2019
// Assignment #6
// Program Name: Flower

// Name of this file: FlowerForm.cs
// Purpose of this file:
// Purpose of this entire program: This program draws a flower.

// Source files in this program: FlowerForm.cs, FlowerMain.cs
// The source files in this program should be compiled in the order specified below in order to satisfy dependencies.
//  1. FlowerForm.cs compiles into library file FlowerForm.dll
//  2. FlowerMain.cs compiles and links with the dll file above to create Flower.exe
// Compile (and link) this file:
// mcs

using System;
using System.Windows.Forms;
using System.Drawing;
using System.Timers;

public class FlowerForm : Form
{
  private const short formWidth = 1600;
  private const short formHeight = formWidth*9/16;
  private const float refreshRate = 1000/20;   //20 frames per second
  private const float animationRate = 1000/20; //20 circles per second

  private Label title = new Label();
  private Button startButton = new Button();
  private Label coordinates = new Label();
  private Button pauseButton = new Button();
  private Button exitButton = new Button();

  private System.Timers.Timer refreshClock = new System.Timers.Timer(refreshRate);
  private System.Timers.Timer animationClock = new System.Timers.Timer(animationRate);

  private System.Drawing.Bitmap pointer_to_bitmap;
  private System.Drawing.Graphics pointer_to_graphics;

  public FlowerForm()
  {
    // Set up texts
    Text = "Flower";
    title.Text = "Flower by Anthony Sam";
    startButton.Text = "Go";
    coordinates.Text = "X:\nY:";
    pauseButton.Text = "Pause";
    exitButton.Text = "Exit";

    // Set up sizes
    Size = new Size(formWidth, formHeight);
    title.Size = new Size(formWidth, formHeight/20);
    coordinates.AutoSize = true;

    // Set up locations
    startButton.Location = new Point(formWidth/5, formHeight*19/20);
    coordinates.Location = new Point(formWidth*2/5, formHeight*19/20);
    pauseButton.Location = new Point(formWidth*3/5, startButton.Top);
    exitButton.Location = new Point(formWidth*4/5, startButton.Top);

    // Set up colors
    BackColor = Color.Orange;
    title.BackColor = Color.Cyan;

    // Add Controls to the form
    Controls.Add(title);
    Controls.Add(startButton);
    Controls.Add(coordinates);
    Controls.Add(pauseButton);
    Controls.Add(exitButton);

    pointer_to_bitmap = new Bitmap(1200, 700, System.Drawing.Imaging.PixelFormat.Format24bppRgb);
    pointer_to_graphics = Graphics.FromImage(pointer_to_bitmap);
  }

  protected override void OnPaint(PaintEventArgs e)
  {
    Graphics graphics = e.Graphics;
    graphics.FillRectangle(Brushes.Yellow, 0, formHeight*9/10, formWidth, formHeight/10);
    base.OnPaint(e);
  }
}
