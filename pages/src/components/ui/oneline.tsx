import type { PingStatistic } from "../ping";
import clsx from "clsx/lite";

function OneLinePage({ remoteAddress, pingStatistics }:
  {
    remoteAddress: string | null;
    pingStatistics: Map<number, PingStatistic>;
  }) {

  const firstPingStatistic = Array.from(pingStatistics).find((x) => x)

  if (!firstPingStatistic) {
    return (
      <div className="font-inter text-sm">
        <div className="text-center text-custom-gold text-shadow-custom-gold">Bad configuration</div>
      </div>
    )
  }

  const period = firstPingStatistic[0]
  const stat = firstPingStatistic[1]

  return (
    <div className="font-inter text-sm">
      {/* Ping status oneline */}
      <table>
        <tbody>
          <tr className="text-right">
            <td className="text-custom-gold text-shadow-custom-gold">Server:</td>
            <td className="text-custom-blue text-shadow-custom-blue pl-1 pr-4 tabular-nums">{remoteAddress || 'Unknown'}</td>
            <td className="text-custom-gold text-shadow-custom-gold px-1">{period} Avg:</td>
            <td className="text-custom-gold text-shadow-custom-gold">Loss:</td>
            <td className={
              clsx(
                stat.lossWarningLevel == "Critical" ? "text-custom-critical text-shadow-custom-critical"
                  : stat.lossWarningLevel == "Warning" ? "text-custom-warning text-shadow-custom-warning"
                    : "text-custom-blue text-shadow-custom-blue",
                "px-3 min-w-[4.0rem] tabular-nums"
              )}>
              {stat.loss.toFixed(0)} %
            </td>
            <td className="text-custom-gold text-shadow-custom-gold">RTT:</td>
            <td className={
              clsx(
                stat.rttWarningLevel == "Critical" ? "text-custom-critical text-shadow-custom-critical"
                  : stat.rttWarningLevel == "Warning" ? "text-custom-warning text-shadow-custom-warning"
                    : "text-custom-blue text-shadow-custom-blue",
                "px-3 min-w-[4.5rem] tabular-nums"
              )}>
              {stat.rtt.toFixed(1)} ms
            </td>
            <td className="text-custom-gold text-shadow-custom-gold">Jitter:</td>
            <td className={
              clsx(
                stat.jitterWarningLevel == "Critical" ? "text-custom-critical text-shadow-custom-critical"
                  : stat.jitterWarningLevel == "Warning" ? "text-custom-warning text-shadow-custom-warning"
                    : "text-custom-blue text-shadow-custom-blue",
                "px-3 min-w-[4.5rem] tabular-nums"
              )}>
              {stat.jitter.toFixed(1)} ms
            </td>
          </tr>
        </tbody>
      </table>
    </div>
  )
}

export default OneLinePage
