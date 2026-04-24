//using Microsoft.AspNetCore.Mvc;
//using System.Text.RegularExpressions;

//[ApiController]
//[Route("api")]
//public class RecommendController : ControllerBase
//{
//	private static Dictionary<string, List<string>> chat = new();
//	private static Dictionary<string, int> step = new();
//	private static Dictionary<string, Dictionary<string, string>> data = new();

//	[HttpPost("recommend")]
//	public IActionResult Chat([FromBody] ChatRequest req)
//	{
//		string userId = "1";

//		if (!chat.ContainsKey(userId))
//		{
//			chat[userId] = new List<string>();
//			step[userId] = 0;
//			data[userId] = new Dictionary<string, string>();
//		}

//		string text = Normalize(req.Text);
//		chat[userId].Add(text);

//		//  البداية
//		if (step[userId] == 0)
//		{
//			step[userId] = 1;

//			return Ok(new
//			{
//				message =
//@"أهلاً عزيزتي في إشراق ✨
//أنا هنا عشان أفهم بشرتك وأعطيك روتين يناسبك 💖

//خلينا نبدأ 
//كيف توصفين بشرتك؟
//(جافة / دهنية / حساسة / عادية / ما أعرف)",
//				isFinal = false
//			});
//		}

//		//  STEP 1: نوع البشرة
//		if (step[userId] == 1)
//		{
//			data[userId]["skin"] = DetectSkin(text);
//			step[userId] = 2;

//			return Ok(new
//			{
//				message = SmartReplySkin(text),
//				isFinal = false
//			});
//		}

//		// 🧠 STEP 2: مشاكل البشرة
//		if (step[userId] == 2)
//		{
//			data[userId]["issue"] = text;
//			step[userId] = 3;

//			return Ok(new
//			{
//				message = "تمام ✨ كم مرة تستخدمين منتجات العناية يومياً؟",
//				isFinal = false
//			});
//		}

//		// 🧠 STEP 3: الروتين
//		if (step[userId] == 3)
//		{
//			data[userId]["routine"] = text;
//			step[userId] = 4;

//			return Ok(new
//			{
//				message = "رائع ✨ أي نوع إطلالة تفضلين؟ (طبيعية / فخمة)",
//				isFinal = false
//			});
//		}

//		//  STEP 4: النتيجة
//		if (step[userId] == 4)
//		{
//			data[userId]["style"] = text;

//			var result = GenerateResult(data[userId]);

//			chat[userId].Clear();
//			data[userId].Clear();
//			step[userId] = 0;

//			return Ok(new
//			{
//				message = result,
//				isFinal = true
//			});
//		}

//		return Ok(new { message = "كملّي ✨", isFinal = false });
//	}

//	//  ذكاء فهم البشرة 
//	private string DetectSkin(string text)
//	{
//		if (Contains(text, "جافة", "تقشر", "ناشفة", "شد"))
//			return "dry";

//		if (Contains(text, "دهنية", "تلمع", "زيت", "دهون"))
//			return "oily";

//		if (Contains(text, "حساسة", "تحسس", "تتهيج", "تحمر"))
//			return "sensitive";

//		if (Contains(text, "ما اعرف", "مدري", "مو عارفة"))
//			return "unknown";

//		return "normal";
//	}

//	// 💬 رد ذكي حسب الإجابة
//	private string SmartReplySkin(string text)
//	{
//		if (Contains(text, "جافة", "تقشر"))
//			return "تمام ✨ واضح إن بشرتك تحتاج ترطيب 💧 هل الجفاف دائم أو بعد الغسيل؟";

//		if (Contains(text, "دهنية", "تلمع"))
//			return "حلو ✨ بشرتك دهنية 🧴 هل تظهر الحبوب أو اللمعة بسرعة؟";

//		if (Contains(text, "حساسة"))
//			return "أفهمك 💖 البشرة الحساسة تحتاج عناية لطيفة هل تتحسس من المنتجات؟";

//		if (Contains(text, "ما اعرف", "مدري"))
//			return "ولا يهمك 💖 قولي لي: هل بشرتك تميل للجفاف أو اللمعة أو الحبوب؟";

//		return "تمام ✨ خلينا نكمل 👇 هل عندك مشاكل مثل حبوب أو جفاف؟";
//	}

//	// 🧠 النتيجة النهائية
//	private string GenerateResult(Dictionary<string, string> d)
//	{
//		string skin = d.ContainsKey("skin") ? d["skin"] : "normal";

//		string result =
//@"✨ تحليل إشراق النهائي

//🌸 نوع البشرة: " + skin + "\n\n";

//		if (skin == "dry")
//		{
//			result +=
//@"💧 التوصيات:
//- غسول لطيف
//- Hyaluronic Acid
//- مرطب ثقيل
//- تجنب الماء الحار";
//		}
//		else if (skin == "oily")
//		{
//			result +=
//@"🧴 التوصيات:
//- غسول للتحكم بالزيوت
//- Niacinamide
//- مرطب خفيف
//- واقي شمس غير دهني";
//		}
//		else if (skin == "sensitive")
//		{
//			result +=
//@"🌿 التوصيات:
//- بدون عطور
//- مهدئات مثل Aloe Vera
//- تجنب التقشير";
//		}
//		else
//		{
//			result +=
//@"💖 التوصيات:
//- روتين متوازن
//- تنظيف + ترطيب + واقي شمس";
//		}

//		result += "\n\n💄 الإطلالة: " +
//			(skin == "oily" ? "مكياج مطفي وثابت" : "طبيعي ناعم");

//		result += "\n\n💖 إشراق يفهمك من كلامك الحقيقي مو اختيارات ثابتة";

//		return result;
//	}

//	// 🔥 دالة ذكاء للكلمات
//	private bool Contains(string text, params string[] words)
//	{
//		foreach (var w in words)
//			if (text.Contains(w)) return true;

//		return false;
//	}

//	private string Normalize(string text)
//	{
//		return text?.ToLower().Trim() ?? "";
//	}
//}

//public class ChatRequest
//{
//	public string Text { get; set; }
//}





using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api")]
public class RecommendController : ControllerBase
{
	private static Dictionary<string, int> step = new();
	private static Dictionary<string, Dictionary<string, string>> data = new();

	[HttpPost("recommend")]
	public IActionResult Chat([FromBody] ChatRequest req)
	{
		string userId = "1"; // بسيط للمدرسة

		if (!step.ContainsKey(userId))
		{
			step[userId] = 0;
			data[userId] = new Dictionary<string, string>();
		}

		string text = Normalize(req.Text);

		// STEP 0
		if (step[userId] == 0)
		{
			step[userId] = 1;

			return Ok(new
			{
				message = "أهلاً في إشراق ✨ كيف بشرتك؟ (جافة / دهنية / حساسة / عادية / ما أعرف)",
				isFinal = false
			});
		}

		// STEP 1: skin
		if (step[userId] == 1)
		{
			string skin = DetectSkin(text);
			data[userId]["skin"] = skin;
			step[userId] = 2;

			return Ok(new
			{
				message = SmartSkinReply(text, skin),
				isFinal = false
			});
		}

		// STEP 2: problem
		if (step[userId] == 2)
		{
			data[userId]["problem"] = text;
			step[userId] = 3;

			return Ok(new
			{
				message = AnalyzeProblem(text),
				isFinal = false
			});
		}

		// STEP 3: routine
		if (step[userId] == 3)
		{
			data[userId]["routine"] = text;
			step[userId] = 4;

			return Ok(new
			{
				message = "رائع ✨ تفضلين مكياج يومي ولا مناسبات؟",
				isFinal = false
			});
		}

		// STEP 4: final
		if (step[userId] == 4)
		{
			string skin = data[userId]["skin"];

			string result = GenerateFinal(skin);

			step[userId] = 0;
			data[userId].Clear();

			return Ok(new
			{
				message = result,
				isFinal = true
			});
		}

		return Ok(new { message = "كملّي ✨", isFinal = false });
	}

	// ===================== SKIN DETECTION =====================
	private string DetectSkin(string text)
	{
		text = text.ToLower();

		if (text.Contains("جافة") || text.Contains("تقشر") || text.Contains("شد"))
			return "dry";

		if (text.Contains("دهنية") || text.Contains("تلمع") || text.Contains("زيت"))
			return "oily";

		if (text.Contains("حساسة") || text.Contains("تحسس") || text.Contains("تحمر"))
			return "sensitive";

		if (text.Contains("ما اعرف") || text.Contains("مدري"))
			return "unknown";

		return "normal";
	}

	// ===================== SMART SKIN REPLY =====================
	private string SmartSkinReply(string text, string skin)
	{
		if (skin == "dry")
			return Pick(new List<string>
			{
				"💧 واضح إن بشرتك تحتاج ترطيب عميق، جربي سيروم الهيالورونيك",
				"💧 الجفاف غالباً من الغسول القوي، استخدمي مرطب ثقيل",
				"💧 أهم شيء: غسول لطيف + مرطب قوي + ماء كثير"
			});

		if (skin == "oily")
			return Pick(new List<string>
			{
				"🧴 بشرتك دهنية، استخدمي غسول ساليسيليك لتنظيم الدهون",
				"🧴 مرطب خفيف بدون زيوت مهم جداً",
				"🧴 قللي المنتجات الثقيلة على البشرة"
			});

		if (skin == "sensitive")
			return Pick(new List<string>
			{
				"🌿 البشرة الحساسة تحتاج منتجات بدون عطور",
				"🌿 استخدمي مكونات مهدئة مثل Aloe Vera",
				"🌿 تجنبي التقشير القوي"
			});

		return "تمام ✨ خلينا نفهم أكثر عن مشكلتك";
	}

	// ===================== PROBLEM ANALYSIS =====================
	private string AnalyzeProblem(string text)
	{
		text = text.ToLower();

		if (text.Contains("حبوب") && text.Contains("دهني"))
			return "🧠 واضح عندك حبوب مع دهون → تحتاجين تنظيف عميق + Niacinamide";

		if (text.Contains("حبوب") && text.Contains("جاف"))
			return "🧠 الحبوب مع الجفاف تعني ضعف حاجز البشرة → نحتاج ترطيب أقوى";

		if (text.Contains("ماكياج") || text.Contains("ميكب"))
			return MakeupAdvice();

		return Pick(new List<string>
		{
			"✨ فهمت عليك، خلينا نخصص لك روتين أفضل",
			"✨ تمام، واضح إن بشرتك تحتاج تنظيم بسيط",
			"✨ خلينا نحل المشكلة خطوة خطوة"
		});
	}

	// ===================== MAKEUP ADVICE =====================
	private string MakeupAdvice()
	{
		return Pick(new List<string>
		{
			"💄 البشرة الدهنية → فاونديشن مطفي + بودرة تثبيت",
			"💄 البشرة الجافة → فاونديشن مرطب + هايلايتر خفيف",
			"💄 الإطلالة اليومية → ألوان ناعمة وطبيعية",
			"💄 المناسبات → كونتور خفيف + روج ثابت"
		});
	}

	// ===================== FINAL RESULT =====================
	private string GenerateFinal(string skin)
	{
		string baseText = "✨ تحليل إشراق النهائي\n\n";

		if (skin == "dry")
			baseText += "💧 روتينك: ترطيب عميق + سيروم + كريم ثقيل";
		else if (skin == "oily")
			baseText += "🧴 روتينك: غسول زيتي + نياسيناميد + مرطب خفيف";
		else if (skin == "sensitive")
			baseText += "🌿 روتينك: منتجات لطيفة بدون عطور";
		else
			baseText += "💖 روتينك: تنظيف + ترطيب + واقي شمس";

		baseText += "\n\n💄 المكياج: " + MakeupAdvice();

		baseText += "\n\n✨ إشراق يفهمك بناءً على كلامك مو اختيارات ثابتة";

		return baseText;
	}

	// ===================== HELPERS =====================
	private string Pick(List<string> list)
	{
		return list[new Random().Next(list.Count)];
	}

	private string Normalize(string text)
	{
		return text?.ToLower().Trim() ?? "";
	}
}

public class ChatRequest
{
	public string Text { get; set; }
}