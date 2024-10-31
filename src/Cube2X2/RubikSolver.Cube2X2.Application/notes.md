# Sizes

Max int size: 2,147,483,647
Max long size: 9,223,372,036,854,775,807

# Looking at pieces, not looking at colors

Hash piece position in cube:
formula: n * 3 + k. Max position hash: 23
where n - coordinate [0-7], k - rotation stage [0-2]. Hash range: [0-23]

Total cube view contains 8 pieces and their positions.
Total cube view hash is the combination of all 8 hashes.
Total cube view hash formula: (3*n1+k1)*24^7 + (3*n2+k2)*24^6 + ... + 3*n8+k8. Max hash is 24^8 - 1 = 110,075,314,176 (long)
x1*y^3 + x2*y^2 + x3*y + x4 = y(x1*y^2 + x2*y + x3) + x4 = y(y(y*x1 + x2) + x3) + x4

Piece numbers:
```
      .----------.----------.
     /.      0  /.      1  /|
    / .        / .        / |
   .----------.----------.  |
  /   .   3  /   .   2  /|  .
 /    .     /    .     / | /|
.----------.----------.  |/ |
|     .    |     .    |  .  |
|     . .  | .   .  . |./|. .
|    .  4  |    .  5  |/ | /
.----------.----------.  |/
|  .  .  . |  .   .  .|  .
| .  7     | .  6     | /
|.         |.         |/
.----------.----------.
```

# Looking at colors, not looking at pieces

Each side has set of tiles, each tile has color.
Color numbers: [0-5]
Count of tiles on side: 4
Count of sides: 6

Hash formula for tile (c - color number): c1 * 6^3 + c2 * 6^2 + c3 * 6 + c4 = 216*c1 + 36*c2 + 6*c3 + c4. Max side hash: 6^4 - 1 = 1295
Full view hash is the combination of hashes of all sides.
Hash formula for full view (h - side hash): h1*1296^5 + h2*1296^4 + ... + h6. Max full view hash: 1296^6 - 1 = ~4,738,381,338,321,617,000 (long)

# Full cube tiles view and sides sequence

```
          +----+----+
          |    .    |
          + - -4- - +
          |    .    |
+----+----+----+----+----+----+----+----+
|    .    |    .    |    .    |    .    |
+ - -0- - + - -1- - + - -2- - + - -3- - +
|    .    |    .    |    .    |    .    |
+----+----+----+----+----+----+----+----+
          |    .    |
          + - -5- - +
          |    .    |
          +----+----+
```

# Functions

Core:
- Interaction:
  - Rotate cube (internal format)
- Hashing:
  - Get code by cube (internal format)
  - Get cube (internal format) by code

Application:
- I/O:
  - Read data (external format)
  - Write data (external format)
- Convert:
  - External -> Internal
  - Internal -> External

# Colors indexes order

top - 0
right - 1
front - 2
bottom - 3
left - 4
back - 5