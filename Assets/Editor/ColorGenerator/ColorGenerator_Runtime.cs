using System;

using UnityEngine;

using Macchiato.Core;

/*
 * Macchiato Utility
 * Contact : crestudioplus@gmail.com // Twitter : https://twitter.com/VRC_Macchiato
 */

namespace Macchiato.Utility {

	[Serializable]
	public struct ColorProfile {
		public string Name;
		public string ReferenceColor;
		public Vector3 ColorDelta1;
		public Vector3 ColorDelta2;
		public Vector3 ColorDelta3;
		public Vector3 RimLightDelta;
		public Vector3 RimShadeDelta;
	}

	public struct ColorProfileColor {
		public Color BaseColor;
		public Color ShadowColor1;
		public Color ShadowColor2;
		public Color ShadowColor3;
		public Color RimLightColor;
		public Color RimShadeColor;
	}

	public static class ColorGeneratorUtility {

		public static ColorProfileColor CalculateColors(ColorProfile TargetColorProfile) {
			Color BaseColor = UnityUtility.HexToColor(TargetColorProfile.ReferenceColor);
			return CalculateFromBaseColor(BaseColor, TargetColorProfile);
		}

		public static ColorProfileColor CalculateFromBaseColor(Color BaseColor, ColorProfile TargetColorProfile) {
			Color ShadowColor1 = GetDeltaColor(BaseColor, TargetColorProfile.ColorDelta1, false);
			Color ShadowColor2 = GetDeltaColor(ShadowColor1, TargetColorProfile.ColorDelta2, false);
			Color ShadowColor3 = GetDeltaColor(ShadowColor2, TargetColorProfile.ColorDelta3, false);
			Color RimLightColor = GetDeltaColor(ShadowColor1, TargetColorProfile.RimLightDelta, false);
			Color RimShadeColor = GetDeltaColor(ShadowColor3, TargetColorProfile.RimShadeDelta, false);
			return new ColorProfileColor {
				BaseColor = BaseColor,
				ShadowColor1 = ShadowColor1,
				ShadowColor2 = ShadowColor2,
				ShadowColor3 = ShadowColor3,
				RimLightColor = RimLightColor,
				RimShadeColor = RimShadeColor
			};
		}

		public static ColorProfileColor CalculateFromShadowColor1(Color ShadowColor1, ColorProfile TargetColorProfile) {
			Color BaseColor = GetDeltaColor(ShadowColor1, TargetColorProfile.ColorDelta1, true);
			Color ShadowColor2 = GetDeltaColor(ShadowColor1, TargetColorProfile.ColorDelta2, false);
			Color ShadowColor3 = GetDeltaColor(ShadowColor2, TargetColorProfile.ColorDelta3, false);
			Color RimLightColor = GetDeltaColor(ShadowColor1, TargetColorProfile.RimLightDelta, false);
			Color RimShadeColor = GetDeltaColor(ShadowColor3, TargetColorProfile.RimShadeDelta, false);
			return new ColorProfileColor {
				BaseColor = BaseColor,
				ShadowColor1 = ShadowColor1,
				ShadowColor2 = ShadowColor2,
				ShadowColor3 = ShadowColor3,
				RimLightColor = RimLightColor,
				RimShadeColor = RimShadeColor
			};
		}

		public static ColorProfileColor CalculateFromShadowColor2(Color ShadowColor2, ColorProfile TargetColorProfile) {
			Color ShadowColor1 = GetDeltaColor(ShadowColor2, TargetColorProfile.ColorDelta2, true);
			Color BaseColor = GetDeltaColor(ShadowColor1, TargetColorProfile.ColorDelta1, true);
			Color ShadowColor3 = GetDeltaColor(ShadowColor2, TargetColorProfile.ColorDelta3, false);
			Color RimLightColor = GetDeltaColor(ShadowColor2, TargetColorProfile.RimLightDelta, false);
			Color RimShadeColor = GetDeltaColor(ShadowColor3, TargetColorProfile.RimShadeDelta, false);
			return new ColorProfileColor {
				BaseColor = BaseColor,
				ShadowColor1 = ShadowColor1,
				ShadowColor2 = ShadowColor2,
				ShadowColor3 = ShadowColor3,
				RimLightColor = RimLightColor,
				RimShadeColor = RimShadeColor
			};
		}

		public static ColorProfileColor CalculateFromShadowColor3(Color ShadowColor3, ColorProfile TargetColorProfile) {
			Color ShadowColor2 = GetDeltaColor(ShadowColor3, TargetColorProfile.ColorDelta3, true);
			Color ShadowColor1 = GetDeltaColor(ShadowColor2, TargetColorProfile.ColorDelta2, true);
			Color BaseColor = GetDeltaColor(ShadowColor1, TargetColorProfile.ColorDelta1, true);
			Color RimLightColor = GetDeltaColor(ShadowColor1, TargetColorProfile.RimLightDelta, false);
			Color RimShadeColor = GetDeltaColor(ShadowColor3, TargetColorProfile.RimShadeDelta, false);
			return new ColorProfileColor {
				BaseColor = BaseColor,
				ShadowColor1 = ShadowColor1,
				ShadowColor2 = ShadowColor2,
				ShadowColor3 = ShadowColor3,
				RimLightColor = RimLightColor,
				RimShadeColor = RimShadeColor
			};
		}

		public static ColorProfile CreateColorProfile(string ProfileName, Color BaseColor, Color ShadowColor1, Color ShadowColor2, Color ShadowColor3, Color RimLightColor, Color RimShadeColor) {
			return new ColorProfile {
				Name = ProfileName,
				ReferenceColor = UnityUtility.ColorToHex(BaseColor),
				ColorDelta1 = GetColorDelta(BaseColor, ShadowColor1),
				ColorDelta2 = GetColorDelta(ShadowColor1, ShadowColor2),
				ColorDelta3 = GetColorDelta(ShadowColor2, ShadowColor3),
				RimLightDelta = GetColorDelta(ShadowColor1, RimLightColor),
				RimShadeDelta = GetColorDelta(ShadowColor3, RimShadeColor)
			};
		}

		public static Vector3 GetColorDelta(Color OriginalColor, Color TargetColor) {
			Vector3 OriginalHSV = UnityUtility.ConvertRGBToHSV(OriginalColor);
			Vector3 TargetHSV = UnityUtility.ConvertRGBToHSV(TargetColor);
			float OriginalH = OriginalHSV.x * 360f;
			float TargetH = TargetHSV.x * 360f;
			float DeltaH = Mathf.Round(Mathf.DeltaAngle(OriginalH, TargetH));
			float DeltaS = Mathf.Round((TargetHSV.y - OriginalHSV.y) * 100f);
			float DeltaV = Mathf.Round((TargetHSV.z - OriginalHSV.z) * 100f);
			return new Vector3(DeltaH, DeltaS, DeltaV);
		}

		public static Color GetDeltaColor(Color TargetColor, Vector3 TargetDelta, bool Backward) {
			Vector3 TargetHSV = UnityUtility.ConvertRGBToHSV(TargetColor);
			float DeltaH = TargetDelta.x / 360f;
			float DeltaS = TargetDelta.y / 100f;
			float DeltaV = TargetDelta.z / 100f;
			float Direction = Backward ? -1f : 1f;
			float NewH = Mathf.Repeat(TargetHSV.x + DeltaH * Direction, 1f);
			float NewS = Mathf.Clamp01(TargetHSV.y + DeltaS * Direction);
			float NewV = Mathf.Clamp01(TargetHSV.z + DeltaV * Direction);
			return Color.HSVToRGB(NewH, NewS, NewV);
		}
	}
}
