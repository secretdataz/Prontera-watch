using Discord.Commands;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PronteraWatch
{
    public class CenLabSolver : ModuleBase<SocketCommandContext>
    {
        [Command("cenlab")]
        [Summary("Solve Central Laboratory binary puzzle")]
        public async Task CenLabAsync(
            [Summary("Number to solve")]
        int num = -1, string fmt = "seq")
        {
            if (num == -1)
            {
                DateTime dt = DateTime.Now;
                num = (dt.Day + dt.Month) * 5;
            }
            string msg = "Turn on (from LEFT to RIGHT) ---> ";
            for(int i = 7; i >= 0; i--)
            {
                int bit = num & (1 << i);
                int test = (1 << i);
                if ((num & (1 << i)) == (1 << i))
                {
                    if (fmt == "seq")
                    {
                        msg += (i + 1).ToString() + " ";
                    }
                    else if (fmt == "bin")
                    {
                        msg += "1 ";
                    }
                } else
                {
                    if (fmt == "seq")
                    {
                        msg += "x ";
                    }
                    else if (fmt == "bin")
                    {
                        msg += "0 ";
                    }
                }
            }
            await Context.Channel.SendMessageAsync(msg.TrimEnd());
        }
    }
}
