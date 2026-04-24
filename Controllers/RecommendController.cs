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

اختاري وش تبين:
1- أعرف نوع بشرتي
2- روتين عناية مناسب
3- مكياج يناسبني",
				isFinal = false
			});
		}

		// 🟢 اختيار الهدف
		if (currentState == "choose_goal")
		{
			if (text.Contains("1") || text.Contains("بشرتي"))
			{
				state[userId] = "skin_q1";

				return Ok(new
				{
					message = "تمام ✨ خلينا نعرف نوع بشرتك\n\nهل بشرتك تلمع خلال اليوم؟ (نعم / لا / ما أعرف)",
					isFinal = false
				});
			}

			if (text.Contains("2") || text.Contains("روتين"))
			{
				state[userId] = "routine_skin";

				return Ok(new
				{
					message = "رائع ✨ أول شيء نحدد بشرتك\nكيف توصفينها؟ (جافة / دهنية / حساسة / ما أعرف)",
					isFinal = false
				});
			}

			if (text.Contains("3") || text.Contains("مكياج"))
			{
				state[userId] = "makeup_skin";

				return Ok(new
				{
					message = "حلو 💄 عشان أختار لك مكياج مناسب\nايش نوع بشرتك؟ (جافة / دهنية / حساسة / ما أعرف)",
					isFinal = false
				});
			}

			return Ok(new
			{
				message = "اختاري رقم 1 أو 2 أو 3 عشان أساعدك بشكل أدق ✨",
				isFinal = false
			});
		}

		// 🟢 سيناريو: تحديد البشرة للمكياج
		if (currentState == "makeup_skin")
		{
			string skin = DetectSkin(text);
			data[userId]["skin"] = skin;
			state[userId] = "makeup_style";

			return Ok(new
			{
				message =
$@"تمام 💖

هل تفضلين:
1- مكياج يومي ناعم
2- مكياج مناسبات قوي",
				isFinal = false
			});
		}

		// 🟢 نوع الإطلالة
		if (currentState == "makeup_style")
		{
			string skin = data[userId]["skin"];

			string result = "";

			if (text.Contains("1") || text.Contains("يومي"))
			{
				result =
$@"💄 توصية مكياج يومي:

- كريم أساس خفيف (Tinted Moisturizer)
- بودرة خفيفة لمنع اللمعة
- ألوان ناعمة (وردي / بيج)

💡 مناسب لـ {skin} البشرة";
			}
			else
			{
				result =
$@"💄 توصية مكياج مناسبات:

- كريم أساس تغطية متوسطة إلى عالية
- كونتور خفيف
- روج قوي (نبيذي / أحمر)

💡 مناسب لـ {skin} البشرة";
			}

			state[userId] = "start";
			data[userId].Clear();

			return Ok(new
			{
				message = result + "\n\n✨ إشراق اختارك حسب كلامك الحقيقي",
				isFinal = true
			});
		}

		return Ok(new { message = "كملّي ✨", isFinal = false });
	}

	// 🧠 فهم البشرة
	private string DetectSkin(string text)
	{
		if (text.Contains("دهنية") || text.Contains("تلمع")) return "دهنية";
		if (text.Contains("جافة") || text.Contains("شد")) return "جافة";
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