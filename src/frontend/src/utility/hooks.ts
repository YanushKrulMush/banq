import { RefObject, useCallback, useRef, useState } from "react";
import { useEventListener } from "ahooks";

export function useClientRect (): [DOMRect | null, RefObject<HTMLDivElement>] {
  const [rect, setRect] = useState<DOMRect | null>(null);
  const ref = useRef<HTMLDivElement>(null);

  const handleResize = () => {
    console.log("fff resize");
    if (!ref.current) return;

    setRect(ref.current.getBoundingClientRect());
  };
  handleResize();
  // const ref = useCallback((node) => {
  //   if (node) {
  //     setRect(node.getBoundingClientRect());
  //   }
  // }, []);

  useEventListener("resize", handleResize);
  return [rect, ref];
}