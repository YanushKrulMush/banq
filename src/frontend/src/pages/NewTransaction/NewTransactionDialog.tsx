import {
  Box,
  Button,
  Dialog,
  DialogContent,
  DialogTitle,
  Grid,
  TextField,
} from "@material-ui/core";
import { Controller, useForm } from "react-hook-form";
import React from "react";
import { Currency } from "../../queries/useAccountQuery";
import useNewTransactionMutation from "../../queries/useNewTransactionMutation";

type Props = {
  isOpen: boolean;
  handleClose: () => void;
};

const formDefaultValues = {
  recipientName: "",
  recipientAddress: "",
  recipientAccountNumber: "",
  amount: "",
  // currency: Currency.PLN,
  title: "",
};

const NewTransactionDialog = ({ isOpen, handleClose }: Props) => {
  const { handleSubmit, formState, control } = useForm<
    typeof formDefaultValues
  >({
    defaultValues: formDefaultValues,
    mode: "onChange",
  });

  const mutation = useNewTransactionMutation();

  return (
    <Dialog open={isOpen} onClose={handleClose}>
      <DialogTitle>Nowy przelew</DialogTitle>
      <DialogContent>
        <Box m={1} minWidth={400}>
          <form
            onSubmit={handleSubmit((body) =>
              mutation.mutate(body, {
                onSuccess: () => {
                  handleClose();
                },
              })
            )}
          >
            <Grid
              container
              direction="column"
              justify="center"
              alignItems="flex-start"
              spacing={5}
            >
              <Box m={1}>
                <Controller
                  render={({ field }) => (
                    <TextField
                      variant="outlined"
                      label="Nazwa odbiorcy"
                      {...field}
                    />
                  )}
                  name="recipientName"
                  rules={{ required: true }}
                  control={control}
                />
              </Box>
              <Box m={1}>
                <Controller
                  render={({ field }) => (
                    <TextField
                      variant="outlined"
                      label="Adres odbiorcy"
                      {...field}
                    />
                  )}
                  name="recipientAddress"
                  rules={{ required: true }}
                  control={control}
                />
              </Box>
              <Box m={1}>
                <Controller
                  render={({ field }) => (
                    <TextField
                      variant="outlined"
                      label="Numer konta odbiorcy"
                      {...field}
                    />
                  )}
                  name="recipientAccountNumber"
                  rules={{ required: true }}
                  control={control}
                />
              </Box>
              <Box m={1}>
                <Controller
                  render={({ field }) => (
                    <TextField variant="outlined" label="Kwota" {...field} />
                  )}
                  name="amount"
                  rules={{ required: true }}
                  control={control}
                />
              </Box>
              <Box m={1}>
                <Controller
                  render={({ field }) => (
                    <TextField variant="outlined" label="Tytuł" {...field} />
                  )}
                  name="title"
                  rules={{ required: true }}
                  control={control}
                />
              </Box>
              <Box m={1}>
                <Button type="submit" color="primary">
                  Wykonaj
                </Button>
              </Box>
            </Grid>
          </form>
        </Box>
      </DialogContent>
    </Dialog>
  );
};

export default NewTransactionDialog;
