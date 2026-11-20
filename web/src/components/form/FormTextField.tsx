import {
  Controller,
  type Control,
  type FieldValues,
  type FieldPath,
} from "react-hook-form";
import { TextField, type TextFieldProps } from "@mui/material";

type FormTextFieldProps<T extends FieldValues> = {
  name: FieldPath<T>;
  control: Control<T>;
} & Omit<TextFieldProps, "name">;

export function FormTextField<T extends FieldValues>({
  name,
  control,
  ...props
}: FormTextFieldProps<T>) {
  return (
    <Controller
      name={name}
      control={control}
      render={({ field, fieldState }) => (
        <TextField
          {...field}
          {...props}
          fullWidth
          error={!!fieldState.error}
          helperText={fieldState.error?.message ?? props.helperText}
        />
      )}
    />
  );
}
