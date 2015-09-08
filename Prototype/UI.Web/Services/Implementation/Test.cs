using Events;
using Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UI.Web.Services.Implementation
{
    class Test : IHandler<AppStartup>
    {
        public async Task<bool> HandleAsync(AppStartup e)
        {
            await new Question()
                .ExecuteAsync(
                    async (Answer1 a1) => true,
                    async (Answer2 a2) => true);

            return true;       
        }
    }

    class Oracle : IHandler<Question>
    {
        public async Task<bool> HandleAsync(Question e)
        {
            await e.ReplyAsync(new Answer1());
            await e.ReplyAsync(new Answer2());
            await e.ReplyAsync(new Answer3());            
            return true;     
        }
    }

    class Spy : 
        IHandler<Question>, 
        IHandler<Answer1>, 
        IHandler<Answer2>, 
        IHandler<Answer3>
    {
        public async Task<bool> HandleAsync(Answer2 e)
        {
            return true;
        }

        public async Task<bool> HandleAsync(Answer3 e)
        {
            return true;
        }

        public async Task<bool> HandleAsync(Answer1 e)
        {
            return true;
        }

        public async Task<bool> HandleAsync(Question e)
        {       
            return true;
        }
    }

    class Question
    { }

    class Answer1
    { }

    class Answer2
    { }

    class Answer3
    { }
}
