namespace Retroverse
{
    public class Interpreter
    {
        Story story;
        Conversation conversation;
        int conversationIndex;
        bool running;

        public Interpreter()
        {
            conversationIndex = 0;
            running = false;
        }

        public void Start(Story story, string label = "")
        {
            this.story = story;
            Goto(label);
        }

        public ConversationAction NextAction()
        {
            if (IsRunning())
            {
                ConversationAction action = conversation.actions[conversationIndex++];
                switch (action.type)
                {
                    case ConversationActionType.GOTO:
                        Goto(action.gotoLabel);
                        return NextAction();
                    case ConversationActionType.FUNCTION:
                        RunFunction(action);
                        return NextAction();
                    case ConversationActionType.STOP:
                        running = false;
                        return null;
                    default:
                        return action;
                }
            }
            return null;
        }

        public bool IsRunning()
        {
            return running && conversation != null && conversation.actions != null && conversationIndex < conversation.actions.Count;
        }

        public void Goto(string label)
        {
            running = true;
            story.sections.TryGetValue(label, out conversation);
            conversationIndex = 0;
        }

        public void RunFunction(ConversationAction action)
        {
            action.function.Invoke();
        }
    }
}
