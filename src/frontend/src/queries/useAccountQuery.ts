import { useQuery, UseQueryOptions } from "react-query";
import { useApiClient } from "../providers/ApiClientProvider";

export enum Currency {
  PLN = "PLN",
  EUR = "EUR",
}

export type Account = {
  number: string;
  balance: number;
  currency: Currency;
  openedOn: string;
};

export default function useAccountQuery(
  // startDate?: string,
  // endDate: string | null = null,
  options?: UseQueryOptions<Account>
) {
  const apiClient = useApiClient();

  return useQuery<Account>(
    ["account"],
    async () => {
      const { data } = await apiClient.get("/dotnet-app/method/account");
      return data;
      return {
        number: "50 1090 1014 0000 0712 1981 2874",
        balance: 4512312 / 100,
        currency: Currency.PLN,
        openedOn: "2011-12-06",
      };
    },
    {
      ...options,
      retry: false,
    }
  );
}
