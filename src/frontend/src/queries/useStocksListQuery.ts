import { useQuery, UseQueryOptions } from "react-query";
import { useApiClient } from "../providers/ApiClientProvider";
import { Currency } from "./useAccountQuery";

export type StockData = {
  id: number;
  name: string;
  value: number;
  quantity: number;
  total: number;
};

export type StocksListDto = {
  items: StockData[];
}

export default function useStocksListQuery(
  options?: UseQueryOptions<StocksListDto>
) {
  const apiClient = useApiClient();

  return useQuery<StocksListDto>(
    ["transactions"],
    async () => 
      // const { data } = await apiClient.get("/dotnet-app/method/transactions");
      // return data;
       ({
        items: [
          {
            id: 1,
            name: 'Allegro.eu SA',
            value: 13427,
            quantity: 14,
            total: 14 * 13427
          }
        ],
      })
    ,
    {
      ...options,
      retry: false,
    }
  );
}
