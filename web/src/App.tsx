import { Container, Typography, Button, Alert } from "@mui/material";
import { HotelsList } from "./features/hotels/components/HotelsList";
import { useCreateHotel } from "./features/hotels/hooks/useCreateHotel";

export default function App() {
  const createHotel = useCreateHotel();
  return (
    <Container sx={{ mt: 4 }}>
      <Typography variant="h4" component="h1" gutterBottom>
        Hotele
      </Typography>
      <Button
        variant="contained"
        sx={{ mb: 2 }}
        disabled={createHotel.isPending}
        onClick={() =>
          createHotel.mutate({
            name: "Hotel Testowy",
            city: "Kraków",
            address: "ul. Testowa 1",
            phone: null,
            email: null,
            checkInTime: "15:00:00",
          })
        }
      >
        Dodaj testowy hotel
      </Button>

      {createHotel.error && (
        <Alert severity="error" sx={{ mb: 2 }}>
          {createHotel.error.message}
        </Alert>
      )}
      <HotelsList />
    </Container>
  );
}
