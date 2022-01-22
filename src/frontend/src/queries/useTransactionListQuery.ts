import { useQuery, UseQueryOptions } from "react-query";
import { useApiClient } from "../providers/ApiClientProvider";
import { Currency } from "./useAccountQuery";

export enum TransactionType {
  incomingTransfer = "IncomingTransfer",
  outgoingTransfer = "OutgoingTransfer",
}

export type TransactionDetails = {
  id: number;
  amount: number;
  currency: Currency;
  type: TransactionType;
  receiverOrSender: string;
  date: string;
  description?: string;
};

export type TransactionDetailsListDto = {
  items: TransactionDetails[];
}

export default function useTransactionListQuery(
  options?: UseQueryOptions<TransactionDetailsListDto>
) {
  const apiClient = useApiClient();

  return useQuery<TransactionDetailsListDto>(
    ["transactions"],
    async () => {
      const { data } = await apiClient.get("/dotnet-app/method/transactions");
      return data;
      return {
        items: [
          {
            id: 1,
            amount: 140,
            currency: "PLN",
            type: TransactionType.incomingTransfer,
            receiverOrSender: "Karol Rosol",
            date: "2021-11-27",
            description: "qwe\nqwe"
          }
        ],
      };
    },
    {
      ...options,
      retry: false,
    }
  );
}
