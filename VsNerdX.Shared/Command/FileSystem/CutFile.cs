using System;
using System.Linq;
using System.Windows.Forms;
using VsNerdX.Core;
using static VsNerdX.VsNerdXPackage;

namespace VsNerdX.Command.Navigation
{
    public class CutFile : ICommand
    {
        private readonly IHierarchyControl _hierarchyControl;

        public CutFile(IHierarchyControl hierarchyControl)
        {
            _hierarchyControl = hierarchyControl;
        }

        public ExecutionResult Execute(IExecutionContext executionContext, Keys key)
        {
            var state = CommandState.Handled;
            if (executionContext.Stack.Count > 0 && executionContext.Stack.Last() == Keys.D && key == Keys.D)
            {
                try
                {
                    Dte.ExecuteCommand("Edit.Cut");
                }
                catch (Exception e) { }

                executionContext = executionContext.Clear();
            }
            else if (key == Keys.D)
            {
                executionContext = executionContext.Add(key).With(delayedExecutable: this);
            }
            else
            {
                executionContext = executionContext.Clear();
                state = CommandState.Cleared;
            }

            return new ExecutionResult(executionContext, state);
        }
    }
}
