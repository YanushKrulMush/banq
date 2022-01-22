import { useMutation, useQueryClient } from "react-query";
import { useSnackbar } from "notistack";
import { useApiClient } from "../providers/ApiClientProvider";
import { ReservationSpot } from "./sharedTypes";

type NewTransactionBody = {
  recipientName: string;
  recipientAddress: string;
  recipientAccountNumber: string;
  amount: string;
  title: string;
};

type NewTransactionResponse = {};

export default function useNewTransactionMutation() {
  const queryClient = useQueryClient();
  const { enqueueSnackbar } = useSnackbar();
  const apiClient = useApiClient();

  return useMutation<NewTransactionResponse, never, NewTransactionBody>(
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

        enqueueSnackbar("Zlecenie przelewu zostało dodane", {
          variant: "success",
        });
      },
    }
  );
}
