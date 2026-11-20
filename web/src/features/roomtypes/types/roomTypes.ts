export type RoomType = {
  id: number;
  hotelId: number;
  name: string;
  description: string | null;
  maxOccupancy: number;
  sizeM2: number;
  isActive: boolean;
};
export const roomTypesKeys = {
  all: ["roomTypes"] as const,
};
