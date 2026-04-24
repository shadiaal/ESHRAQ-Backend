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
		string userId = "1";
		string text = Normalize(req.Text);

		if (!step.ContainsKey(userId))
		{
			step[userId] = 0;
			data[userId] = new Dictionary<string, string>();
		}

		// 🟢 البداية
		if (step[userId] == 0)
		{
			step[userId] = 1;

			return Ok(new
			{
				message =
@"أهلاً في إشراق ✨
أنا هنا أفهم بشرتك وأعطيك توصية تناسبك 💖

وش تحبين أعرف لك؟
1- نوع بشرتي
2- روتين مناسب
3- مكياج يناسبني",
				isFinal = false
			});
		}

		// 🟢 اختيار الهدف
		if (step[userId] == 1)
		{
			if (Contains(text, "1", "بشرة"))
				data[userId]["mode"] = "skin";

			else if (Contains(text, "2", "روتين"))
				data[userId]["mode"] = "routine";

			else if (Contains(text, "3", "مكياج"))
				data[userId]["mode"] = "makeup";

			else
				return Ok(new { message = "اختاري 1 أو 2 أو 3 ✨", isFinal = false });

			step[userId] = 2;

			return Ok(new
			{
				message = "كيف تحسين بشرتك غالباً؟ (تلمع / ناشفة / عادية / ما أعرف)",
				isFinal = false
			});
		}

		// 🧠 تحليل أولي
		if (step[userId] == 2)
		{
			data[userId]["feeling"] = DetectFeeling(text);

			step[userId] = 3;

			return Ok(new
			{
				message = "هل تظهر حبوب أو مسام واضحة؟ (نعم / لا)",
				isFinal = false
			});
		}

		// 🧠 مشاكل البشرة
		if (step[userId] == 3)
		{
			data[userId]["acne"] = text.Contains("نعم") ? "yes" : "no";

			step[userId] = 4;

			return Ok(new
			{
				message = "كيف يكون ملمس بشرتك بعد الغسيل؟ (مشدودة / عادية)",
				isFinal = false
			});
		}

		// 🧠 تحليل أعمق
		if (step[userId] == 4)
		{
			data[userId]["afterwash"] = text.Contains("مشدودة") ? "dry" : "normal";

			string skin = AnalyzeSkin(data[userId]);
			data[userId]["skin"] = skin;

			// 🔥 يفرق حسب الهدف
			if (data[userId]["mode"] == "makeup")
			{
				step[userId] = 5;

				return Ok(new
				{
					message = "تحبين المكياج (ناعم / قوي)?",
					isFinal = false
				});
			}

			if (data[userId]["mode"] == "routine")
			{
				step[userId] = 6;

				return Ok(new
				{
					message = GenerateRoutine(skin),
					isFinal = true
				});
			}

			// فقط معرفة البشرة
			step[userId] = 0;
			data[userId].Clear();

			return Ok(new
			{
				message = $"✨ نوع بشرتك: {skin}",
				isFinal = true
			});
		}

		// 💄 المكياج
		if (step[userId] == 5)
		{
			string skin = data[userId]["skin"];
			string result = GenerateMakeup(skin, text);

			step[userId] = 0;
			data[userId].Clear();

			return Ok(new
			{
				message = result,
				isFinal = true
			});
		}

		return Ok(new { message = "ما فهمت عليك، ممكن توضحين أكثر؟ ✨", isFinal = false });
	}

	// 🧠 تحليل البشرة الحقيقي
	private string AnalyzeSkin(Dictionary<string, string> d)
	{
		string feeling = d["feeling"];
		string acne = d["acne"];
		string after = d["afterwash"];

		if (feeling == "oily" && acne == "yes") return "دهنية ومعرضة حبوب";
		if (feeling == "oily") return "دهنية";
		if (after == "dry") return "جافة";
		if (feeling == "normal") return "عادية";

		return "مختلطة";
	}

	// 💄 المكياج
	private string GenerateMakeup(string skin, string style)
	{
		if (style.Contains("ناعم"))
		{
			return
$@"💄 مكياج ناعم لبشرتك {skin}:

- كريم خفيف
- ألوان طبيعية
- لمعة خفيفة";
		}

		return
$@"💄 مكياج قوي لبشرتك {skin}:

- تغطية عالية
- تثبيت قوي
- ألوان جريئة";
	}

	// 💧 روتين
	private string GenerateRoutine(string skin)
	{
		if (skin.Contains("دهنية"))
			return "🧴 غسول + نياسيناميد + مرطب خفيف";

		if (skin.Contains("جافة"))
			return "💧 غسول لطيف + ترطيب عميق + سيروم";

		return "💖 روتين متوازن + واقي شمس";
	}

	// 🧠 فهم الكلام
	private string DetectFeeling(string text)
	{
		if (Contains(text, "تلمع", "زيت")) return "oily";
		if (Contains(text, "ناشفة", "جافة")) return "dry";
		if (Contains(text, "عادية")) return "normal";
		return "unknown";
	}

	private bool Contains(string text, params string[] words)
	{
		foreach (var w in words)
			if (text.Contains(w)) return true;

		return false;
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