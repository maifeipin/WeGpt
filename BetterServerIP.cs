using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace WEBGPT
{
    internal class BetterServerIP
    {

        public static async Task<string> GetBestDomainAsync(string[] domains)
        {
            var tasks = domains.Select(GetPingInfoAsync);
            var results = await Task.WhenAll(tasks);

            double minScore = double.MaxValue;
            string bestDomain = null;

            for (int i = 0; i < results.Length; i++)
            {
                var result = results[i];
                if (result != null)
                {
                    // 计算分数，这里权重设置为丢包率0.7，平均延迟0.3，可以根据具体情况调整
                    double score = result.Item1 * 0.7 + result.Item2 * 0.3;

                    if (score < minScore)
                    {
                        minScore = score;
                        bestDomain = domains[i];
                    }
                }
            }

            return bestDomain;
        }

        public static async Task<Tuple<double, double>> GetPingInfoAsync(string domain)
        {
            ProcessStartInfo startInfo = new ProcessStartInfo()
            {
                FileName = "ping",
                Arguments = $"  -n 10 {domain}",
                RedirectStandardOutput = true,
                CreateNoWindow = true,
                UseShellExecute = false
            };

            using (var process = Process.Start(startInfo))
            {
                string output = await process.StandardOutput.ReadToEndAsync();
                process.WaitForExit();
                if (output.Length == 0) return null;
                var packetLossMatch = Regex.Match(output, @"(\d*\.?\d*)% 丢失");
                if (!packetLossMatch.Success) return null;
                double packetLoss = double.Parse(packetLossMatch.Groups[1].Value);
                if (packetLoss == 100) return null;
                var latencyMatch = Regex.Match(output, @"平均 = (\d*\.?\d*)ms");
                if (!latencyMatch.Success) return null;
                double avgLatency = double.Parse(latencyMatch.Groups[1].Value);

                Tuple<double, double> tuple = new Tuple<double, double>(packetLoss, avgLatency);
                return tuple;
            }
        }
    }
}
