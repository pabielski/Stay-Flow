import { useState } from "react";
import { Button, Container, Stack, Typography } from "@mui/material";
import AddIcon from "@mui/icons-material/Add";
import { HotelsList } from "../features/hotels/components/HotelsList";
import { HotelFormDialog } from "../features/hotels/components/HotelFormDialog";
import type { Hotel } from "../features/hotels/types/hotel";
import { ConfirmDialog } from "../components/ConfirmDialog";
import { useDeactivateHotel } from "../features/hotels/hooks/useDeactivateHotel";
export function HotelsPage() {
  const [isFormOpen, setIsFormOpen] = useState(false);
  const [selectedHotel, setSelectedHotel] = useState<Hotel | null>(null);

  const openCreate = () => {
    setSelectedHotel(null);
    setIsFormOpen(true);
  };

  const openEdit = (hotel: Hotel) => {
    setSelectedHotel(hotel);
    setIsFormOpen(true);
  };
  const [hotelToDeactivate, setHotelToDeactivate] = useState<Hotel | null>(
    null,
  );
  const deactivateHotel = useDeactivateHotel();

  const closeDeactivate = () => {
    setHotelToDeactivate(null);
    deactivateHotel.reset();
  };

  const confirmDeactivate = () => {
    if (!hotelToDeactivate) return;
    deactivateHotel.mutate(hotelToDeactivate.id, {
      onSuccess: closeDeactivate,
    });
  };
  return (
    <Container sx={{ mt: 4 }}>
      <Stack
        direction="row"
        sx={{ justifyContent: "space-between", alignItems: "center", mb: 2 }}
      >
        <Typography variant="h4" component="h1">
          Hotele
        </Typography>
        <Button
          variant="contained"
          startIcon={<AddIcon />}
          onClick={openCreate}
        >
          Dodaj hotel
        </Button>
      </Stack>

      <HotelsList onEdit={openEdit} onDeactivate={setHotelToDeactivate} />

      <HotelFormDialog
        open={isFormOpen}
        hotel={selectedHotel}
        onClose={() => setIsFormOpen(false)}
      />
      <ConfirmDialog
        open={hotelToDeactivate !== null}
        title="Dezaktywacja hotelu"
        message={`Czy na pewno chcesz dezaktywować hotel „${hotelToDeactivate?.name}”? Hotel zniknie z oferty, ale jego dane i historia zostaną zachowane.`}
        confirmLabel="Dezaktywuj"
        onConfirm={confirmDeactivate}
        onClose={closeDeactivate}
        isPending={deactivateHotel.isPending}
        error={deactivateHotel.error?.message ?? null}
      />
    </Container>
  );
}
