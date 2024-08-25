import { useEffect, useState, useMemo } from "react"
import { addOverlayListener, removeOverlayListener } from "../libs/overlay-plugin";
import handleImage from '/src/assets/handle.png';
import clsx from "clsx/lite";
import DefaultPage from "./ui/default";
import OneLinePage from "./ui/oneline";
import { EventMap } from "../libs/overlay-plugin/event";

export type PingResult = {
  timestamp: number
  address: string
  status: string
  rtt: number
  ttl: number
}

export type WarningLevel = 'Normal' | 'Warning' | 'Critical'

export type PingStatistic = {
  address: string
  loss: number
  lossWarningLevel: WarningLevel
  rtt: number
  rttWarningLevel: WarningLevel
  jitter: number
  jitterWarningLevel: WarningLevel
}

function Ping({ ui = 'oneline' }: { ui: 'default' | 'oneline' }) {
  const [isLocked, setIsLocked] = useState(false);
  const [latestPingResult, setLatestPingResult] = useState<PingResult | null>(null);
  const [pingResults, setPingResults] = useState<PingResult[]>([]);

  // Get the aggregation periods from the query string
  const aggregationPeriods = useMemo(() => {
    const queryParams = new URLSearchParams(window.location.search);
    const aggParams = queryParams.get('agg') || queryParams.get('avg'); // 'avg' is for backward compatibility
    const aggPeriods = aggParams ? aggParams.split(',').map((v) => parseInt(v, 10)).filter((x => isFinite(x))) : [5, 30, 60];
    return aggPeriods;
  }, []);

  // Get the maximum number of samples from the aggregation periods
  const maxNumberOfSamples = useMemo(() => Math.max(...aggregationPeriods), [aggregationPeriods]);

  // Calculate the ping statistics
  const pingStatistics = useMemo(() => {
    const stats = new Map<number, PingStatistic>();

    const checkLossWarningLevel = (loss: number, samplesCount: number) => {
      if (samplesCount >= 50) {
        return loss > 5 ? 'Critical' : loss > 1 ? 'Warning' : 'Normal';
      } else if (samplesCount >= 30) {
        return loss > 6 ? 'Critical' : loss > 2 ? 'Warning' : 'Normal';
      } else if (samplesCount >= 20) {
        return loss > 8 ? 'Critical' : loss > 4 ? 'Warning' : 'Normal';
      } else if (samplesCount >= 10) {
        return loss > 10 ? 'Critical' : loss > 5 ? 'Warning' : 'Normal';
      } else if (samplesCount >= 5) {
        return loss > 20 ? 'Critical' : loss > 10 ? 'Warning' : 'Normal';
      } else if (samplesCount > 0) {
        return loss > 50 ? 'Critical' : loss > 25 ? 'Warning' : 'Normal';
      } else {
        return 'Normal';
      }
    }

    const checkRTTWarningLevel = (rtt: number) => {
      return rtt > 50 ? 'Critical' : rtt > 30 ? 'Warning' : 'Normal';
    }

    const checkJitterWarningLevel = (jitter: number, rtt: number) => {
      if (rtt > 50) {
        return jitter / rtt > 0.2 ? 'Critical' : jitter / rtt > 0.1 ? 'Warning' : 'Normal';
      } else if (rtt > 25) {
        return jitter / rtt > 0.3 ? 'Critical' : jitter / rtt > 0.15 ? 'Warning' : 'Normal';
      } else if (rtt > 10) {
        return jitter / rtt > 0.4 ? 'Critical' : jitter / rtt > 0.2 ? 'Warning' : 'Normal';
      } else if (rtt > 0) {
        return jitter / rtt > 0.5 ? 'Critical' : jitter / rtt > 0.25 ? 'Warning' : 'Normal';
      } else {
        return 'Normal';
      }
    }

    for (const period of aggregationPeriods) {
      const targetAddress = latestPingResult?.address || '';
      const ipFiltered = pingResults.filter((v) => v.address === targetAddress);
      const samples = period < ipFiltered.length ? ipFiltered.slice(-period) : ipFiltered;
      const successSamples = samples.filter((v) => v.status === 'Success');
      const loss = samples.length > 0 ? (1.0 - successSamples.length / samples.length) * 100 : 0;
      const lossWarningLevel = checkLossWarningLevel(loss, samples.length);
      const rtt = successSamples.length > 0 ? successSamples.reduce((acc, v) => acc + v.rtt, 0) / successSamples.length : 0;
      const rttWarningLevel = checkRTTWarningLevel(rtt);
      const jitter = successSamples.length > 1 ? Math.sqrt(successSamples.reduce((acc, v) => acc + Math.pow(v.rtt - rtt, 2), 0) / (successSamples.length - 1)) : 0;
      const jitterWarningLevel = checkJitterWarningLevel(jitter, rtt);
      stats.set(period, { address: targetAddress, loss, lossWarningLevel, rtt, rttWarningLevel, jitter, jitterWarningLevel });
    }
    return stats;
  }, [pingResults, aggregationPeriods, latestPingResult]);

  useEffect(() => {
    const handleOverlayStateUpdate = (ev: CustomEvent<{
      isLocked: boolean;
    }>) => {
      setIsLocked(ev.detail.isLocked);
    }

    const handleOnPingStatusUpdateEvent = (ev: Parameters<EventMap['onPingStatusUpdateEvent']>[0]) => {
      if (!ev.detail?.statusJson) {
        return;
      }
      const status = JSON.parse(ev.detail.statusJson);
      const result: PingResult = {
        timestamp: status.Timestamp,
        address: status.Address,
        status: status.Status,
        rtt: status.RTT,
        ttl: status.TTL
      }
      setLatestPingResult(() => result);
      setPingResults((prev) => {
        if (prev.length >= maxNumberOfSamples) {
          prev.shift()
        }
        return [...prev, result]
      })
    };

    document.addEventListener('onOverlayStateUpdate', handleOverlayStateUpdate);
    addOverlayListener('onPingStatusUpdateEvent', handleOnPingStatusUpdateEvent);

    return () => {
      // Cleanup the event listeners
      document.removeEventListener('onOverlayStateUpdate', handleOverlayStateUpdate);
      removeOverlayListener('onPingStatusUpdateEvent', handleOnPingStatusUpdateEvent);
    }
  }, [])

  return (
    <div className={clsx("h-full relative border px-2 py-1", !isLocked ? "border-gray-400" : "border-transparent")}>
      {/* Overlay background and border */}
      <div className="absolute top-0 left-0 w-full h-full rounded-md bg-black/50 blur-[2px] -z-10" />
      {/* Overlay handle */}
      {!isLocked && <img src={handleImage} alt="handle" className="absolute bottom-0 right-0" />}
      {/* Overlay content */}
      {ui === 'default' && <DefaultPage latestPingResult={latestPingResult} pingStatistics={pingStatistics} />}
      {ui === 'oneline' && <OneLinePage latestPingResult={latestPingResult} pingStatistics={pingStatistics} />}
    </div>
  )
}

export default Ping
