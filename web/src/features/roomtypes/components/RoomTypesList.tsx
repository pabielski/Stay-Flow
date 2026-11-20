import { useRoomTypes } from "../hooks/useRoomTypes";
import { CircularProgress, Alert, Chip, Stack } from "@mui/material";

type RoomTypesListProps = {
  hotelId: number;
};
export function RoomTypesList({ hotelId }: RoomTypesListProps) {
  const { roomTypes, isLoading, error } = useRoomTypes(hotelId);
  if (isLoading) return <CircularProgress size={14} />;
  if (error) return <Alert severity="error">{error}</Alert>;

  return (
    <>
      <Stack direction="row" spacing={1} useFlexGap sx={{ flexWrap: "wrap" }}>
        {roomTypes.map((roomType) => (
          <Chip key={roomType.id} label={roomType.name} size="small" />
        ))}
      </Stack>
    </>
  );
}
