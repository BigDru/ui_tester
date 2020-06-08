using System;
using System.Diagnostics;
using System.Threading;
using System.Windows.Automation;
using System.Windows.Forms;

namespace ui_tester
{
    class Program
    {
        static void Main(string[] args)
        {
            Process calc = System.Diagnostics.Process.Start("calc");
            // actual calculator will have another process id
            calc.WaitForExit();
            Thread.Sleep(100);

            AutomationElement calc_automation_element = FindChildElement(
                "Calculator",
                AutomationElement.RootElement,
                TreeScope.Children);
            answer_to_the_ultimate_question_of_life_the_universe_and_everything(calc_automation_element);
        }

        private static void answer_to_the_ultimate_question_of_life_the_universe_and_everything(
            AutomationElement calc_automation_element)
        {
            AutomationElement clear = FindChildElement("Clear", calc_automation_element);
            AutomationElement six = FindChildElement("Six", calc_automation_element);
            AutomationElement seven = FindChildElement("Seven", calc_automation_element);
            AutomationElement multiply = FindChildElement("Multiply by", calc_automation_element);

            InvokePattern clear_invoke = clear.GetCurrentPattern(InvokePattern.Pattern) as InvokePattern;
            InvokePattern six_invoke = six.GetCurrentPattern(InvokePattern.Pattern) as InvokePattern;
            InvokePattern seven_invoke = seven.GetCurrentPattern(InvokePattern.Pattern) as InvokePattern;
            InvokePattern multiply_invoke = multiply.GetCurrentPattern(InvokePattern.Pattern) as InvokePattern;

            clear_invoke.Invoke();
            six_invoke.Invoke();
            multiply_invoke.Invoke();
            seven_invoke.Invoke();

            SendKeys.SendWait("{ENTER}");
        }

        // src: https://docs.microsoft.com/en-us/dotnet/api/system.windows.automation.automationelement.findfirst?view=netcore-3.1
        private static AutomationElement FindChildElement(
            String controlName,
            AutomationElement rootElement,
            TreeScope scope = TreeScope.Descendants)
        {
            if ((controlName == "") || (rootElement == null))
            {
                throw new ArgumentException("Argument cannot be null or empty.");
            }
            // Set a property condition that will be used to find the main form of the
            // target application. In the case of a WinForms control, the name of the control
            // is also the AutomationId of the element representing the control.
            Condition propCondition = new PropertyCondition(
                AutomationElement.NameProperty, controlName, PropertyConditionFlags.IgnoreCase);

            // Find the element.
            return rootElement.FindFirst(scope, propCondition);
        }

        // // src: https://docs.microsoft.com/en-us/dotnet/framework/ui-automation/navigate-among-ui-automation-elements-with-treewalker
        // private void WalkEnabledElements(AutomationElement rootElement, TreeNode treeNode)
        // {
        //     Condition condition1 = new PropertyCondition(AutomationElement.IsControlElementProperty, true);
        //     Condition condition2 = new PropertyCondition(AutomationElement.IsEnabledProperty, true);
        //     TreeWalker walker = new TreeWalker(new AndCondition(condition1, condition2));
        //     AutomationElement elementNode = walker.GetFirstChild(rootElement);
        //     while (elementNode != null)
        //     {
        //         TreeNode childTreeNode = treeNode.Nodes.Add(elementNode.Current.ControlType.LocalizedControlType);
        //         WalkEnabledElements(elementNode, childTreeNode);
        //         elementNode = walker.GetNextSibling(elementNode);
        //     }
        // }
    }
}
