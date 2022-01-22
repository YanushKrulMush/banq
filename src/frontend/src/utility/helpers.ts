import { format } from "date-fns";

export const formatDateTime = (date: Date) => format(date, "R-MM-dd'T'kk:mm':00.000Z'");
