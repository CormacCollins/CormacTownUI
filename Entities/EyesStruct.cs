using System;
using SwinGameSDK;

namespace MyGame
{
	
	
	public class EyesStruct
	{
		//EYE 1
		LineSegment eye1Line1;		
		Point2D eye1Line1Part1;
		Point2D eye1Line1Part2;
		
		LineSegment eye1Line2;
		Point2D eye1Line2Part1;
		Point2D eye1Line2Part2;
		
		//EYE 2
		LineSegment eye2Line1;
		//Line 1 in left eye cross
		Point2D eye2Line1Part1;
		Point2D eye2Line1Part2;
		
		LineSegment eye2Line2;
		Point2D eye2Line2Part1;
		Point2D eye2Line2Part2;
		
		public EyesStruct(Point2D p1, Point2D p2, Point2D p3, Point2D p4, Point2D p5, Point2D p6, Point2D p7, Point2D p8){
			eye1Line1 = new LineSegment();
			eye1Line1Part1 = p1;
			eye1Line1Part2 = p2;			
			eye1Line1.StartPoint = p1;
			eye1Line1.EndPoint = p2;
			eye1Line2 = new LineSegment();
			eye1Line2Part1 = p3;
			eye1Line2Part2 = p4;
			eye1Line2.StartPoint = p3;
			eye1Line2.EndPoint = p4;
			
			eye2Line1 = new LineSegment();
			eye2Line1Part1 = p5;
			eye2Line1Part2 = p6;			
			eye2Line1.StartPoint = p5;
			eye2Line1.EndPoint = p6;
			eye2Line2 = new LineSegment();
			eye2Line2Part1 = p7;
			eye2Line2Part2 = p8;
			eye2Line2.StartPoint = p7;
			eye2Line2.EndPoint = p8;
			
			
		}
		
		public void UpdateEyePoints(){
			
		}
		
		public LineSegment Eye1Line1{
			get{return eye1Line1;}
			set{eye1Line1 = value;}
		}

		public Point2D Eye1Line1Part1{
			get{return eye1Line1Part1;}
			set{eye1Line1Part1 = value;}
		}

		public Point2D Eye1Line1Part2{
			get{return eye1Line1Part2;}
			set{eye1Line1Part2 = value;}
		}

		public LineSegment Eye1Line2{
			get{return eye1Line2;}
			set{eye1Line2 = value;}
		}

		public Point2D Eye1Line2Part1{
			get{return eye1Line2Part1;}
			set{eye1Line2Part1 = value;}
		}

		public Point2D Eye1Line2Part2{
			get{return eye1Line2Part2;}
			set{eye1Line2Part2 = value;}
		}

		public LineSegment Eye2Line1{
			get{return eye2Line1;}
			set{eye2Line1 = value;}
		}

		public Point2D Eye2Line1Part1{
			get{return eye2Line1Part1;}
			set{eye2Line1Part1 = value;}
		}

		public Point2D Eye2Line1Part2{
			get{return eye2Line1Part2;}
			set{eye2Line1Part2 = value;}
		}

		public LineSegment Eye2Line2{
			get{return eye2Line2;}
			set{eye2Line2 = value;}
		}

		public Point2D Eye2Line2Part1{
			get{return eye2Line2Part1;}
			set{eye2Line2Part1 = value;}		
		}

		public Point2D Eye2Line2Part2{
			get{return eye2Line2Part2;}
			set{eye2Line2Part2 = value;}
		}
	}
}

