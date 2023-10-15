
using Newtonsoft.Json;
using RulesEngine.Models;
using RulesEngineSample;

var rulesStr = @"[{
	""WorkflowName"": ""Test"",	
	""GlobalParams"": [{
		""Name"": ""discountValue"",
		""Expression"": ""discountA.Value""
	}],
	""Rules"": [{
			""RuleName"": ""CheckAge"",
			""Expression"": ""buyerA.Age < 18"",
			""Actions"": {
				""OnSuccess"": {
					""Name"": ""OutputExpression"",
					""Context"": {
						""Expression"": ""discountValue * 0.9""
					}
				}
			}
		},
		{
			""RuleName"": ""CheckVIP"",
			""Expression"": ""vipA.IsVIP == true"",
			""Actions"": {
				""OnSuccess"": {
					""Name"": ""OutputExpression"",
					""Context"": {
						""Expression"": ""discountValue * 0.9""
					}
				}
			}
		}
	]
}]";

var rp1 = new RuleParameter("buyerA", new Buyer
{
    Id = 666,
    Age = 16
});

var rp2 = new RuleParameter("vipA", new VIP
{
    Id = 666,
    IsVIP = true
});

var rp3 = new RuleParameter("discountA", new Discount
{
    Value = 1.0
});


var workflows = JsonConvert.DeserializeObject<List<Workflow>>(rulesStr)!;
var bre = new RulesEngine.RulesEngine(workflows.ToArray());

List<RuleResultTree> resultList = await bre.ExecuteAllRulesAsync("Test", rp1,rp2,rp3);
var discount = 1.0;
foreach (var item in resultList)
{
    if (item.ActionResult != null && item.ActionResult.Output != null)
    {
        Console.WriteLine($"{item.Rule.RuleName} Discount Offer：{item.ActionResult.Output}");
        discount = discount * (double)item.ActionResult.Output;
    }
}
Console.WriteLine($"Final Discount Offer:{discount}");

