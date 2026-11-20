import { useEffect } from "react";
import { useForm } from "react-hook-form";
import { yupResolver } from "@hookform/resolvers/yup";
import {
  Alert,
  Button,
  Dialog,
  DialogActions,
  DialogContent,
  DialogTitle,
  Stack,
} from "@mui/material";
import { FormTextField } from "../../../components/form/FormTextField";
import { hotelSchema, type HotelFormValues } from "../validation/hotelSchema";
import type { CreateHotelRequest, Hotel } from "../types/hotel";
import { useCreateHotel } from "../hooks/useCreateHotel";
import { useUpdateHotel } from "../hooks/useUpdateHotel";

type HotelFormDialogProps = {
  open: boolean;
  onClose: () => void;
  hotel?: Hotel | null;
};

const emptyValues: HotelFormValues = {
  name: "",
  city: "",
  address: "",
  phone: "",
  email: "",
  checkInTime: "15:00",
};

function fromHotel(hotel: Hotel): HotelFormValues {
  return {
    name: hotel.name,
    city: hotel.city,
    address: hotel.address,
    phone: hotel.phone ?? "",
    email: hotel.email ?? "",
    checkInTime: hotel.checkInTime.slice(0, 5),
  };
}

function toRequest(values: HotelFormValues): CreateHotelRequest {
  return {
    name: values.name,
    city: values.city,
    address: values.address,
    phone: values.phone || null,
    email: values.email || null,
    checkInTime:
      values.checkInTime.length === 5
        ? `${values.checkInTime}:00`
        : values.checkInTime,
  };
}

export function HotelFormDialog({
  open,
  onClose,
  hotel,
}: HotelFormDialogProps) {
  const isEdit = !!hotel;

  const { control, handleSubmit, reset } = useForm<HotelFormValues>({
    resolver: yupResolver(hotelSchema),
    defaultValues: emptyValues,
  });

  const createHotel = useCreateHotel();
  const updateHotel = useUpdateHotel();
  const activeMutation = isEdit ? updateHotel : createHotel;

  useEffect(() => {
    if (open) {
      reset(hotel ? fromHotel(hotel) : emptyValues);
    }
  }, [open, hotel, reset]);

  const handleClose = () => {
    createHotel.reset();
    updateHotel.reset();
    onClose();
  };

  const onSubmit = (values: HotelFormValues) => {
    const request = toRequest(values);

    if (hotel) {
      updateHotel.mutate({ id: hotel.id, request }, { onSuccess: handleClose });
    } else {
      createHotel.mutate(request, { onSuccess: handleClose });
    }
  };

  return (
    <Dialog open={open} onClose={handleClose} fullWidth maxWidth="sm">
      <form onSubmit={handleSubmit(onSubmit)} noValidate>
        <DialogTitle>{isEdit ? "Edycja hotelu" : "Nowy hotel"}</DialogTitle>

        <DialogContent>
          <Stack spacing={2} sx={{ mt: 1 }}>
            {activeMutation.error && (
              <Alert severity="error">{activeMutation.error.message}</Alert>
            )}

            <FormTextField
              control={control}
              name="name"
              label="Nazwa"
              required
            />
            <FormTextField
              control={control}
              name="city"
              label="Miasto"
              required
            />
            <FormTextField
              control={control}
              name="address"
              label="Adres"
              required
            />
            <FormTextField control={control} name="phone" label="Telefon" />
            <FormTextField
              control={control}
              name="email"
              label="E-mail"
              type="email"
            />
            <FormTextField
              control={control}
              name="checkInTime"
              label="Godzina zameldowania"
              type="time"
              required
              slotProps={{ inputLabel: { shrink: true } }}
            />
          </Stack>
        </DialogContent>

        <DialogActions>
          <Button onClick={handleClose}>Anuluj</Button>
          <Button
            type="submit"
            variant="contained"
            disabled={activeMutation.isPending}
          >
            Zapisz
          </Button>
        </DialogActions>
      </form>
    </Dialog>
  );
}
