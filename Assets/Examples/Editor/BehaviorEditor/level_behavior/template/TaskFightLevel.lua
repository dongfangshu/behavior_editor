---@class TaskFightLevel
local level = BaseClass("TaskFightLevel")
function level:__init( ... )
	-- body

	---@field levelId number
	self.levelId = 1003

	-- ---@class MonsterInfo
	-- ---@field entityId number
	-- ---@field position_id string
	-- ---@field position_name string

	-- ---@field monsterLevel MonsterInfo[]
	-- self.monsterLevel ={ 
	-- 	{ Id = nil,entityId = 120121,position_id = "",position_name = ""},
	-- 	{ Id = nil,entityId = 120121,position_id = "",position_name = ""}

	-- }
	---@field checkRole boolean
	self.checkRole = false
end
function level:Init( ... )
	-- body
end
function level:LateInit( ... )
	-- body
end
function level:Update( ... )
	-- body
end
function level:Remove( ... )
	-- body
end
function level:__delete( ... )
	-- body
end
---@function
function level:GetEntity()
	-- body
end


