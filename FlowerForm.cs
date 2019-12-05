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
// mcs -target:library -r:System.Drawing.dll -r:System.Windows.Forms.dll -out:FlowerForm.dll FlowerForm.cs

using System;
using System.Windows.Forms;
using System.Drawing;
using System.Timers;

public class FlowerForm : Form
{
  // Create Controls
  private Label title = new Label();
  private Label controlPanel = new Label();
  private Button startButton = new Button();
  private Label coordinates = new Label();
  private Button pauseButton = new Button();
  private Button exitButton = new Button();

  // Create Timers
  private const float refreshRate = 1000/20; //20 frames per second
  private const float animationRate = 1000/200; //200 dots per second
  private System.Timers.Timer refreshClock = new System.Timers.Timer(refreshRate);
  private System.Timers.Timer animationClock = new System.Timers.Timer(animationRate);

  // These will be used in drawing the dots
  private const short radius = 4;
  private double t = 0; //this is theta; it's used in calculating each dots' coordinates
  private PointF myPoint;

  private System.Drawing.Bitmap flowerBitmap; //the bitmap where the flower will be drawn on
  private System.Drawing.Graphics flowerGraphics; //used to draw the flower on the bitmap

  public FlowerForm()
  {
    // Set up texts
    Text = "Flower";
    title.Text = "Flower by Anthony Sam";
    title.TextAlign = ContentAlignment.MiddleCenter;
    controlPanel.Text = "Control Panel";
    controlPanel.TextAlign = ContentAlignment.TopCenter;
    startButton.Text = "Go";
    coordinates.Text = "X:\nY:";
    pauseButton.Text = "Pause";
    exitButton.Text = "Exit";

    // Set up sizes
    Height = 900;
    Width = Height*16/9;
    title.Size = new Size(Width, Height/10);
    controlPanel.Size = title.Size;
    coordinates.AutoSize = true;

    // Set up locations
    controlPanel.Location = new Point(0, Height-controlPanel.Height);
    startButton.Location = new Point(Width/5, Height*19/20-startButton.Height/2);
    coordinates.Location = new Point(Width*2/5, Height*19/20-coordinates.Height/2);
    pauseButton.Location = new Point(Width*3/5, startButton.Top);
    exitButton.Location = new Point(Width*4/5, startButton.Top);

    // Set up colors
    BackColor = Color.Magenta;
    title.BackColor = Color.Cyan;
    controlPanel.BackColor = Color.Yellow;

    // Add Controls to the form
    Controls.Add(title);
    Controls.Add(startButton);
    Controls.Add(coordinates);
    Controls.Add(pauseButton);
    Controls.Add(exitButton);
    Controls.Add(controlPanel);

    // Define how methods are called
    startButton.Click += new EventHandler(start);
    pauseButton.Click += new EventHandler(pause);
    exitButton.Click += new EventHandler(exit);
    refreshClock.Elapsed += new ElapsedEventHandler(refresh);
    animationClock.Elapsed += new ElapsedEventHandler(update);

    flowerBitmap = new Bitmap(Width, Height, System.Drawing.Imaging.PixelFormat.Format24bppRgb);
    flowerGraphics = Graphics.FromImage(flowerBitmap);

    flowerGraphics.FillRectangle(Brushes.LawnGreen, 0, Height/2, Width, 1); //draw the x-axis
    flowerGraphics.FillRectangle(Brushes.LawnGreen, Width/2, 0, 1, Height); //draw the y-axis
  }

  protected override void OnPaint(PaintEventArgs e)
  {
    Graphics graphics = e.Graphics;

    graphics.DrawImage(flowerBitmap, 0, 0, Width, Height); //copy the bitmap over

    base.OnPaint(e);
  }

  protected void start(Object sender, EventArgs e)
  {
    refreshClock.Start();
    animationClock.Start();
  }

  protected void pause(Object sender, EventArgs e)
  {
    refreshClock.Stop();
    animationClock.Stop();
  }

  protected void exit(Object sender, EventArgs e)
  {
    System.Console.WriteLine("You clicked on the Exit button.");
    Close();
  }

  protected void refresh(Object sender, ElapsedEventArgs events)
  {
    Invalidate(); //call OnPaint()
    coordinates.Text = "X: " + (myPoint.X-Width/2) + "\nY: " + (-(myPoint.Y-Height/2));
  }

  protected void update(Object sender, ElapsedEventArgs events)
  {
    if(t >= 2*Math.PI)
    {
      // Stop the clocks
      refreshClock.Stop();
      animationClock.Stop();

      // Call OnPaint() and update the coordinates one last time (this could be necessary if refreshClock.Interval != animationClock.Interval)
      Invalidate();
      coordinates.Text = "X: " + (myPoint.X-Width/2) + "\nY: " + (myPoint.Y-Height/2);
    }
    else
    {
      // Calculate the location of the dot
      myPoint.X = (float)(Width/2 + (Height/2-title.Height-radius)*Math.Cos(2*t)*Math.Cos(t));  //X(t) = 800 + 428*cos(2t)*cos(t) ==> X'(t) = 428*-2sin(2t)*cos(t) + 432*cos(2t)*-sin(t) = -214 (sin(t) + 3 sin(3 t))
      myPoint.Y = (float)(Height/2 - (Height/2-title.Height-radius)*Math.Cos(2*t)*Math.Sin(t)); //Y(t) = 450 - 428*cos(2t)*sin(t) ==> Y'(t) = 428*-2sin(2t)*sin(t) + 432*cos(2t)*cos(t)  =  214 (cos(t) - 3 cos(3 t))

      // Draw the dot on the bitmap
      flowerGraphics.FillEllipse(Brushes.DarkOrchid, myPoint.X-radius, myPoint.Y-radius, radius*2, radius*2);

      // Increment theta
      t += radius/4 / Math.Sqrt(Math.Pow(-(Height/4-title.Height/2)*(Math.Sin(t) + 3*Math.Sin(3*t)), 2) + Math.Pow((Height/4-title.Height/2)*(Math.Cos(t) - 3*Math.Cos(3*t)), 2)); //distancePerTick / ||<X'(t), Y'(t)>||
    }
  }
}
