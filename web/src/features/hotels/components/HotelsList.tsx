import {
  Alert,
  Chip,
  CircularProgress,
  Paper,
  Table,
  TableBody,
  TableCell,
  TableContainer,
  TableHead,
  TableRow,
  IconButton,
  Tooltip,
  Switch,
  FormControlLabel,
} from "@mui/material";
import { useHotels } from "../hooks/useHotels";
import EditIcon from "@mui/icons-material/Edit";
import type { Hotel } from "../types/hotel";
import BlockIcon from "@mui/icons-material/Block";
import { useState } from "react";
import { RoomTypesList } from "../../roomtypes/components/RoomTypesList";
type HotelsListProps = {
  onEdit: (hotel: Hotel) => void;
  onDeactivate: (hotel: Hotel) => void;
};

export function HotelsList({ onEdit, onDeactivate }: HotelsListProps) {
  const [showInactiveHotels, setShowInactiveHotels] = useState(false);
  const { hotels, isLoading, error } = useHotels({
    includeInactive: showInactiveHotels,
  });
  if (isLoading) return <CircularProgress />;
  if (error) return <Alert severity="error">{error}</Alert>;

  return (
    <>
      <FormControlLabel
        control={
          <Switch
            checked={showInactiveHotels}
            onChange={(event) => setShowInactiveHotels(event.target.checked)}
          />
        }
        label="Pokaż nieaktywne"
        sx={{ mb: 1 }}
      />
      <TableContainer component={Paper}>
        <Table>
          <TableHead>
            <TableRow>
              <TableCell>Nazwa</TableCell>
              <TableCell>Miasto</TableCell>
              <TableCell>Pokoje</TableCell>
              <TableCell>Adres</TableCell>
              <TableCell>Status</TableCell>
              <TableCell align="right">Akcje</TableCell>
            </TableRow>
          </TableHead>
          <TableBody>
            {hotels.map((hotel) => (
              <TableRow key={hotel.id}>
                <TableCell>{hotel.name}</TableCell>
                <TableCell>{hotel.city}</TableCell>
                <TableCell>
                  <RoomTypesList hotelId={hotel.id} />
                </TableCell>
                <TableCell>{hotel.address}</TableCell>
                <TableCell>
                  {hotel.isActive ? (
                    <Chip label="Aktywny" color="success" size="small" />
                  ) : (
                    <Chip label="Nieaktywny" size="small" />
                  )}
                </TableCell>
                <TableCell align="right">
                  <Tooltip title="Edytuj">
                    <IconButton
                      aria-label={`Edytuj hotel ${hotel.name}`}
                      onClick={() => onEdit(hotel)}
                    >
                      <EditIcon />
                    </IconButton>
                  </Tooltip>
                  {hotel.isActive && (
                    <Tooltip title="Dezaktywuj">
                      <IconButton
                        aria-label={`Dezaktywuj hotel ${hotel.name}`}
                        color="error"
                        onClick={() => onDeactivate(hotel)}
                      >
                        <BlockIcon />
                      </IconButton>
                    </Tooltip>
                  )}
                </TableCell>
              </TableRow>
            ))}
          </TableBody>
        </Table>
      </TableContainer>
    </>
  );
}
