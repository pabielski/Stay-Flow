import {
  ActivityIndicator,
  FlatList,
  StyleSheet,
  Text,
  View,
} from "react-native";
import { useHotels } from "../hooks/useHotels";

export function HotelsList() {
  const { hotels, isLoading, error, isRefreshing, refresh } = useHotels();

  if (isLoading) {
    return <ActivityIndicator size="large" style={styles.center} />;
  }

  if (error) {
    return <Text style={styles.error}>{error}</Text>;
  }

  return (
    <FlatList
      data={hotels}
      keyExtractor={(hotel) => String(hotel.id)}
      refreshing={isRefreshing}
      onRefresh={refresh}
      contentContainerStyle={styles.list}
      renderItem={({ item }) => (
        <View style={styles.card}>
          <Text style={styles.name}>{item.name}</Text>
          <Text style={styles.details}>
            {item.city}, {item.address}
          </Text>
          <Text style={item.isActive ? styles.active : styles.inactive}>
            {item.isActive ? "Aktywny" : "Nieaktywny"}
          </Text>
        </View>
      )}
    />
  );
}

const styles = StyleSheet.create({
  center: { flex: 1 },
  list: { padding: 16, gap: 12 },
  card: { backgroundColor: "#ffffff", borderRadius: 8, padding: 16 },
  name: { fontSize: 18, fontWeight: "600", color: "#1c2a38" },
  details: { marginTop: 4, color: "#5d6b78" },
  active: { marginTop: 8, color: "#3f7457", fontWeight: "500" },
  inactive: { marginTop: 8, color: "#9aa7b3", fontWeight: "500" },
  error: { margin: 16, color: "#a33b3b" },
});
