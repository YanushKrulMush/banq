import { useMutation, useQueryClient } from "react-query";
import { useSnackbar } from "notistack";
import { useApiClient } from "../providers/ApiClientProvider";
import { ReservationSpot } from "./sharedTypes";

type BuyOrderBody = {
  stock: string;
  quantity: string;
};

type SellOrderResponse = {};

export default function useBuyOrderMutation() {
  const queryClient = useQueryClient();
  const { enqueueSnackbar } = useSnackbar();
  const apiClient = useApiClient();

  return useMutation<SellOrderResponse, never, BuyOrderBody>(
    async (dto) => {
      // const { data } = await apiClient.post("api/reservations", dto);
      // return data;
      console.log(dto);
      return {};
    },
    {
      onSuccess: (reservation) => {
        // const reservationSpots =
        //   queryClient.getQueryData<ReservationSpot[]>("reservationSpots") ?? [];
        //
        // const index = reservationSpots.findIndex(
        //   ({ id }) => id < reservation.id
        // );
        //
        // queryClient.setQueryData("reservationSpots", [
        //   ...reservationSpots.slice(0, index),
        //   reservation,
        //   ...reservationSpots.slice(index),
        // ]);

        enqueueSnackbar("Zlecenie sprzedaży zostało stworzone", {
          variant: "success",
        });
      },
    }
  );
}
