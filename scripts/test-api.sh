#!/bin/bash

BASE_URL="https://localhost:5001"
API_URL="$BASE_URL/api"

echo "🚀 Testing ClothesShop API"
echo "Base URL: $BASE_URL"
echo ""

# Test 1: Get all products
echo "1. Testing GET /api/products"
curl -s -X GET "$API_URL/products" \
  -H "Content-Type: application/json" | jq .

echo -e "\n2. Testing GET /api/products/1"
curl -s -X GET "$API_URL/products/1" \
  -H "Content-Type: application/json" | jq .

echo -e "\n✅ API Tests Complete!"
