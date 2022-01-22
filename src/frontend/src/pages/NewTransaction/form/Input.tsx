import { Control, Controller } from "react-hook-form";
import { TextField } from "@material-ui/core";
import React from "react";

type Props = {
  name: string;
  label: string;
  type?: React.InputHTMLAttributes<unknown>["type"];
  control: Control;
};

const Input = ({ name, label, type, control }: Props) => (
    <Controller
      render={({ field }) => (
        <TextField variant="outlined" label={label} type={type} {...field} />
      )}
      name={name}
      rules={{ required: true }}
      control={control}
    />
  );

Input.defaultProps = {
  type: undefined,
};

export default Input;
