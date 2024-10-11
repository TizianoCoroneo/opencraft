package model

import (
	"fmt"

	"github.com/g3n/engine/math32"
)

// length, width, and height of a chunk expressed in number of blocks.
const chunkSize uint = 16

// Height of columns expressed in number of Chunks.
const columnHeight uint = 64

// 2D position used to determine the location of Columns.
type UintPos2 struct {
	X, Y uint
}

type IntPos2 struct {
	X, Y int
}

// 3D position used to determine the location of Blocks.
type UintPos3 struct {
	X, Y, Z uint
}

type IntPos3 struct {
	X, Y, Z int
}

// Chunks are a cube of blocks of the specified size.
type Chunk struct {
	Data [chunkSize * chunkSize * chunkSize]uint8
}

// Translates a global block position into the location of the column that
// contains that block.
func TranslateWorldPosToColumnCoords(pos IntPos3) (IntPos2, error) {
	if pos.Y > int(chunkSize*columnHeight) {
		return IntPos2{}, fmt.Errorf("y higher than world")
	}
	return IntPos2{pos.X / int(chunkSize), pos.Z / int(chunkSize)}, nil
}

// Translates a global block position to a block position inside a column.
func TranslateLocWorldToColumn(pos IntPos3) (UintPos3, error) {
	if pos.Y >= int(chunkSize*columnHeight) {
		return UintPos3{}, fmt.Errorf("y higher than world")
	}
	nx := uint(mod(pos.X, int(chunkSize)))
	ny := uint(pos.Y)
	nz := uint(mod(pos.Z, int(chunkSize)))
	return UintPos3{nx, ny, nz}, nil
}

// Translate a block position inside a column to a position inside a chunk.
func TranslateLocColumnToChunk(pos UintPos3) (UintPos3, error) {
	if pos.X >= chunkSize {
		return UintPos3{}, fmt.Errorf("x wider than column")
	}
	if pos.Z >= chunkSize {
		return UintPos3{}, fmt.Errorf("z wider than column")
	}
	return UintPos3{pos.X, pos.Y % chunkSize, pos.Z}, nil
}

// Set the type of all blocks in the chunk to the given type.
func (c *Chunk) SetTerrainType(t uint8) {
	for i := range c.Data {
		c.Data[i] = t
	}
}

// Get the array index of the block at the given location.
func (c *Chunk) GetBlockIndex(loc UintPos3) (int, error) {
	ix := loc.X
	iz := loc.Z * chunkSize
	iy := loc.Y * chunkSize * chunkSize
	i := int(ix + iz + iy)
	if i > len(c.Data) {
		return 0, fmt.Errorf("loc not in chunk")
	}
	return i, nil
}

// Set the block type of the block at the given location.
func (c *Chunk) SetBlockType(loc UintPos3, t uint8) error {
	i, err := c.GetBlockIndex(loc)
	if err != nil {
		return err
	}
	c.Data[i] = t
	return nil
}

// Get the block type of the block at the given location.
func (c *Chunk) GetBlockType(loc UintPos3) (uint8, error) {
	i, err := c.GetBlockIndex(loc)
	if err != nil {
		return 0, err
	}
	return c.Data[i], nil
}

// Columns are stacks of the specified number of chunks.
type Column struct {
	Data [columnHeight]*Chunk
}

// Set the type of all blocks in the column to the given type.
func (c *Column) SetTerrainType(t uint8) {
	for i := range c.Data {
		c.Data[i].SetTerrainType(t)
	}
}

// Get the chunk at the given index, and create it if it does not exist.
func (c *Column) ChunkAtIndex(i int) (*Chunk, error) {
	if i >= len(c.Data) {
		return nil, fmt.Errorf("loc not in column")
	}
	res := c.Data[i]
	if res == nil {
		res = &Chunk{}
		c.Data[i] = res
	}
	return res, nil
}

// Set the block type of the block at the given location to the given type.
func (c *Column) SetBlockType(loc UintPos3, t uint8) error {
	i := int(loc.Y / chunkSize)
	chunk, err := c.ChunkAtIndex(i)
	if err != nil {
		return err
	}
	chunkLoc, err := TranslateLocColumnToChunk(loc)
	if err != nil {
		return err
	}
	if err := chunk.SetBlockType(chunkLoc, t); err != nil {
		return err
	}
	return nil
}

// Get the block type of the block at the given location.
func (c *Column) GetBlockType(loc UintPos3) (uint8, error) {
	i := int(loc.Y / chunkSize)
	chunk, err := c.ChunkAtIndex(i)
	if err != nil {
		return 0, err
	}
	chunkLoc, err := TranslateLocColumnToChunk(loc)
	if err != nil {
		return 0, err
	}
	res, err := chunk.GetBlockType(chunkLoc)
	if err != nil {
		return 0, err
	}
	return res, nil
}

// Describes the terrain of the world.
type Terrain struct {
	Data map[IntPos2]*Column
}

// Initializes the Terrain struct.
func NewTerrain() *Terrain {
	return &Terrain{
		make(map[IntPos2]*Column),
	}
}

func mod(a, b int) int {
	return (a%b + b) % b
}

// Set the type of the block at the given location to the given type.
func (t *Terrain) SetBlockType(loc IntPos3, typ uint8) error {
	coords, err := TranslateWorldPosToColumnCoords(loc)
	if err != nil {
		return err
	}
	column := t.Data[coords]
	if column == nil {
		column = &Column{}
		t.Data[coords] = column
	}
	columnLoc, err := TranslateLocWorldToColumn(loc)
	if err != nil {
		return err
	}
	return column.SetBlockType(columnLoc, typ)
}

// Get the type of the block at the given location.
func (t *Terrain) GetBlockType(loc IntPos3) (uint8, error) {
	coords, err := TranslateWorldPosToColumnCoords(loc)
	if err != nil {
		return 0, err
	}
	column := t.Data[coords]
	if column == nil {
		column = &Column{}
		t.Data[coords] = column
	}
	columnLoc, err := TranslateLocWorldToColumn(loc)
	if err != nil {
		return 0, err
	}
	return column.GetBlockType(columnLoc)
}

// A character, player controller or otherwise.
type Character struct {
	Position      math32.Vector3
	Width, Height uint
}

// Compute a bounding box for this avatar.
func (c *Character) BoundingBox() *math32.Box3 {
	halfWidth := float32(c.Width) / 2
	halfHeight := float32(c.Height) / 2
	maxVec := math32.Vector3{
		X: c.Position.X + halfWidth,
		Y: c.Position.Y + halfHeight,
		Z: c.Position.Z + halfWidth,
	}
	minVec := math32.Vector3{
		X: c.Position.X - halfWidth,
		Y: c.Position.Y,
		Z: c.Position.Z - halfWidth,
	}
	return math32.NewBox3(&minVec, &maxVec)
}

// Describes a player
type Player struct {
	Character
}

// A unique identifier for a player.
type PlayerID uint

// A unique identifier for a type of item.
type ItemID uint

// Item represents an in-game item.
type ItemInstance struct {
	Position math32.Vector3
	ID       ItemID
}

// The world: terrain with players in it.
type World struct {
	Terrain
	// A map of player IDs to player structs.
	PlayerMap map[PlayerID]*Player
}

// Initializes the world struct.
func NewWorld() *World {
	return &World{
		Terrain:   *NewTerrain(),
		PlayerMap: make(map[PlayerID]*Player),
	}
}
