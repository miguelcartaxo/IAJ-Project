using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.IAJ.Unity.Movement.DynamicMovement
{
	public class Cohesion : DynamicArrive
	{
		public Vector3 massCenter { get; set; }
		public float Radius { get; set; }
		public float FanAngle { get; set; }
		public List<DynamicCharacter> Flock { get; set; }

		public Cohesion ()
		{
		}

		public float ShortestAngleDifference(float source, float target){
			float delta = target - source;
			if (delta > Math.PI)
				delta -= 2 * (float)Math.PI;
			else if (delta < Math.PI)
				delta += 2 * (float)Math.PI;

			return delta;
		}

		public override MovementOutput GetMovement()
		{

			int closeBoids = 0;
			Vector3 direction;
			float angle, angleDifference;

			foreach (var boid in Flock) {

				if (Character != boid.KinematicData) {
					direction = boid.KinematicData.position - Character.position;

					if (direction.magnitude <= Radius) {
						angle = Util.MathHelper.ConvertVectorToOrientation (direction);
						angleDifference = ShortestAngleDifference (Character.orientation, angle);

						if (Mathf.Abs (angleDifference) <= FanAngle) {
							massCenter += boid.KinematicData.position;
							closeBoids++;
						}
					}
				}
			}

			if(closeBoids == 0)
				return new MovementOutput();

			massCenter /= closeBoids;
			Target.position = massCenter;

			return base.GetMovement();

		}
	}
}

