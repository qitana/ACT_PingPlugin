import type { PingStatistic, PingResult } from "../ping";
import clsx from "clsx/lite";

function DefaultPage({ pingStatistics, latestPingResult }:
  {
    pingStatistics: Map<number, PingStatistic>;
    latestPingResult: PingResult | null;
  }) {
  return (
    <div className="font-inter text-sm">
      {/* Ping status header */}
      <div className="flex gap-2">
        <div className="space-x-1">
          <span className="text-custom-gold text-shadow-custom-gold">Server:</span>
          <span className="text-custom-blue text-shadow-custom-blue tabular-nums">{latestPingResult?.address}</span>
        </div>
        <div className="space-x-1">
          <span className="text-custom-gold text-shadow-custom-gold">TTL:</span>
          <span className="text-custom-blue text-shadow-custom-blue tabular-nums">{latestPingResult?.ttl}</span>
        </div>
      </div>
      {/* Ping statistics */}
      <div className="ml-2">
        <table>
          <tbody>
            {Array.from(pingStatistics).map(([period, stat]) => (
              <tr className="text-right">
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
                    "px-3 min-w-[4.75rem] tabular-nums"
                  )}>
                  {stat.rtt.toFixed(1)} ms
                </td>
                <td className="text-custom-gold text-shadow-custom-gold">Jitter:</td>
                <td className={
                  clsx(
                    stat.jitterWarningLevel == "Critical" ? "text-custom-critical text-shadow-custom-critical"
                      : stat.jitterWarningLevel == "Warning" ? "text-custom-warning text-shadow-custom-warning"
                        : "text-custom-blue text-shadow-custom-blue",
                    "px-3 min-w-[4.75rem] tabular-nums"
                  )}>
                  {stat.jitter.toFixed(1)} ms
                </td>
              </tr>
            ))}
          </tbody>
        </table>
      </div>
    </div>
  )
}

export default DefaultPage
