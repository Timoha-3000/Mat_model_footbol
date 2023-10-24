using System;
using System.Drawing;
using System.Linq;
using System.Collections.Generic;
using System.Globalization;
using System.Reflection.Metadata.Ecma335;

namespace task_1
{
    class Ball
    {
        public float speed { get; } = 10;
        public float angel { get; } = 30;

        public Ball() { }
    }

    class Footbolist
    {
        public Point position { get; set; }
        public float light { get; set; }
    }

    public class Field
    {
        public bool[,] list = new bool[10, 30];
        public List<Point> listPoints = new List<Point>();
        public int accuracyHieght = 10;
        public int accuracyWight = 30;
        int accuracy = 100;

        public Field()
        {
            for (int i = 0; i < accuracyHieght; i++)
            {
                for (int j = 0; j < accuracyWight; j++)
                    listPoints.Add(new Point(i, j));
            }

            for (int i = 0; i < accuracyHieght; i++)
            {
                for (int j = 0; j < accuracyWight; j++)
                    list[i, j] = false;
            }
        }

        public void WriteField(bool[,] list)
        {
            for (int x = 0; x < accuracyHieght; x++)
            {
                for (int y = 0; y < accuracyWight; y++)
                {
                    if (list[x, y])
                    {
                        Console.ForegroundColor = ConsoleColor.Gray;
                        Console.Write($"1");
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.Write($"0");
                    }

                }

                Console.Write("\n");
            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Ball ball = new Ball();
            Footbolist footbolist = new Footbolist();
            Field field = new Field();

            //Console.WriteLine("Введите расстояние от футболисто до ворот:");
            //footbolist.light = Console.Read();
            Console.WriteLine("              |||");

            for (int i = 0; i < field.accuracyWight; i++)
            {
                for (int j = 0; j < field.accuracyHieght; j++)
                {
                    Point gate = new Point(field.accuracyWight / 2, 0);
                    field.list[j,i] = СalculationHandker.GoalCheck(ball.speed, ball.angel, СalculationHandker.LeghtLine(new Point(i, j), gate), 2);
                }
            }

            field.WriteField(field.list);
        }
    }

    public static class СalculationHandker
    {
        public static double LeghtLine(Point point1, Point point2)
        {
            return Math.Sqrt(Math.Pow(point2.X - point1.X, 2) + Math.Pow(point2.Y - point1.Y, 2));
        }

        public static bool GoalCheck(double velocity, double angle, double distance, double gateH)
        {
            double g = 9.81;
            double t1 = (velocity * Math.Abs(Math.Sin(angle))) / (g);
            double L = Math.Abs(velocity * Math.Cos(angle) * 2 - distance);
            double y = g * (L) / (velocity * Math.Cos(angle));
            double hMax = velocity * Math.Abs(Math.Sin(angle)) * t1;
            double h = hMax - y;


            if (gateH > h)
                return true;
            else 
                return false;
        }
    }
}