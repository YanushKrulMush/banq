import {
  Box,
  Button,
  Dialog,
  DialogContent,
  DialogTitle, FormControl,
  Grid, InputLabel, MenuItem, Select,
  TextField
} from "@material-ui/core";
import { Controller, useForm } from "react-hook-form";
import React from "react";
import useSellOrderMutation from "../../queries/useSellOrderMutation";
import Autocomplete from "@material-ui/lab/Autocomplete";

type Props = {
  isOpen: boolean;
  handleClose: () => void;
};

const formDefaultValues = {
  quantity: "",
  stock: "allegro",
};


const SellOrderDialog = ({ isOpen, handleClose }: Props) => {
  const { handleSubmit, formState, control } = useForm<
    typeof formDefaultValues
  >({
    defaultValues: formDefaultValues,
    mode: "onChange",
  });

  const mutation = useSellOrderMutation();

  return (
    <Dialog open={isOpen} onClose={handleClose}>
      <DialogTitle>Zlecenie sprzedaży</DialogTitle>
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
                    <FormControl variant="outlined">
                      <InputLabel>Nazwa</InputLabel>
                      <Select
                        label="Nazwa"
                        {...field}
                      >
                        <MenuItem value="allegro">Allegro.eu SA</MenuItem>
                        <MenuItem value="lotos">Grupa Lotos SA</MenuItem>
                        <MenuItem value="orange">Orange Polska SA</MenuItem>
                      </Select>
                    </FormControl>
                  )}
                  name="stock"
                  rules={{ required: true }}
                  control={control}
                />
              </Box>
              <Box m={1}>
                <Controller
                  render={({ field }) => (
                    <TextField variant="outlined" label="Liczba" {...field} />
                  )}
                  name="quantity"
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

export default SellOrderDialog;
