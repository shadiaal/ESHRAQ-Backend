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
	private static Dictionary<string, string> state = new();
	private static Dictionary<string, Dictionary<string, string>> data = new();

	[HttpPost("recommend")]
	public IActionResult Chat([FromBody] ChatRequest req)
	{
		string userId = "1";
		string text = Normalize(req.Text);

		if (!state.ContainsKey(userId))
		{
			state[userId] = "start";
			data[userId] = new Dictionary<string, string>();
		}

		string currentState = state[userId];

		// 🟢 البداية
		if (currentState == "start")
		{
			state[userId] = "choose_goal";

			return Ok(new
			{
				message =
@"أهلاً في إشراق ✨
أنا هنا أساعدك تفهمين بشرتك وتختارين روتين ومكياج مناسب 💄

اختاري:
1- أعرف نوع بشرتي
2- مكياج يناسبني",
				isFinal = false
			});
		}

		// 🟢 اختيار الهدف
		if (currentState == "choose_goal")
		{
			if (text.Contains("1") || text.Contains("بشرة"))
			{
				state[userId] = "skin_q1";

				return Ok(new
				{
					message = "هل بشرتك تلمع خلال اليوم؟ (نعم / لا / ما أعرف)",
					isFinal = false
				});
			}

			if (text.Contains("2") || text.Contains("مكياج"))
			{
				state[userId] = "makeup_skin";

				return Ok(new
				{
					message = "ايش نوع بشرتك؟ (جافة / دهنية / حساسة / ما أعرف)",
					isFinal = false
				});
			}

			return Ok(new { message = "اختاري 1 أو 2 ✨", isFinal = false });
		}

		// 🟢 تحليل البشرة - سؤال 1
		if (currentState == "skin_q1")
		{
			if (text.Contains("نعم"))
				data[userId]["oil"] = "yes";
			else if (text.Contains("لا"))
				data[userId]["oil"] = "no";
			else
				data[userId]["oil"] = "unknown";

			state[userId] = "skin_q2";

			return Ok(new
			{
				message = "هل تحسين بشرتك مشدودة أو ناشفة بعد الغسيل؟ (نعم / لا / ما أعرف)",
				isFinal = false
			});
		}

		// 🟢 تحليل البشرة - سؤال 2
		if (currentState == "skin_q2")
		{
			if (text.Contains("نعم"))
				data[userId]["dry"] = "yes";
			else if (text.Contains("لا"))
				data[userId]["dry"] = "no";
			else
				data[userId]["dry"] = "unknown";

			string oil = data[userId]["oil"];
			string dry = data[userId]["dry"];

			string skinType = "";

			if (oil == "yes" && dry == "no")
				skinType = "دهنية";
			else if (oil == "no" && dry == "yes")
				skinType = "جافة";
			else if (oil == "yes" && dry == "yes")
				skinType = "مختلطة";
			else
				skinType = "عادية";

			state[userId] = "start";
			data[userId].Clear();

			return Ok(new
			{
				message =
$@"✨ نوع بشرتك: {skinType}

💡 روتين مناسب:
- غسول مناسب لنوع بشرتك
- مرطب يومي
- واقي شمس ☀️

✨ إشراق حلل بشرتك بناءً على إجاباتك",
				isFinal = true
			});
		}

		// 🟢 مكياج - تحديد البشرة
		if (currentState == "makeup_skin")
		{
			string skin = DetectSkin(text);
			data[userId]["skin"] = skin;

			if (skin == "غير محدد")
			{
				state[userId] = "skin_q1";

				return Ok(new
				{
					message = "طيب خلينا نحددها ✨ هل بشرتك تلمع خلال اليوم؟ (نعم / لا)",
					isFinal = false
				});
			}

			state[userId] = "makeup_style";

			return Ok(new
			{
				message = "تحبين مكياج: (يومي / مناسبات)",
				isFinal = false
			});
		}

		// 🟢 اختيار نوع المكياج
		if (currentState == "makeup_style")
		{
			string skin = data[userId]["skin"];
			string result = "";

			if (text.Contains("يومي"))
			{
				result =
$@"💄 مكياج يومي مناسب لبشرتك {skin}:

- كريم أساس خفيف
- بودرة خفيفة
- ألوان ناعمة (وردي / بيج)

💡 يعطيك لوك طبيعي ومرتب";
			}
			else
			{
				result =
$@"💄 مكياج مناسبات مناسب لبشرتك {skin}:

- كريم أساس تغطية أعلى
- تثبيت قوي
- ألوان جريئة

💡 مناسب للسهرات والتصوير";
			}

			state[userId] = "start";
			data[userId].Clear();

			return Ok(new
			{
				message = result + "\n\n✨ إشراق اختار لك حسب بشرتك",
				isFinal = true
			});
		}

		return Ok(new { message = "ما فهمت عليك، جربي تكتبين بشكل أوضح ✨", isFinal = false });
	}

	private string DetectSkin(string text)
	{
		if (text.Contains("دهنية")) return "دهنية";
		if (text.Contains("جافة")) return "جافة";
		if (text.Contains("حساسة")) return "حساسة";
		return "غير محدد";
	}

	private string Normalize(string text)
	{
		return text?.Trim() ?? "";
	}
}

public class ChatRequest
{
	public string Text { get; set; }
}